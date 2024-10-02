﻿namespace Authentication.API.Features.Login
{
    public record LoginCommand(string UserName, string Password) : ICommand<LoginResult>;
    public record LoginResult(TokenModel Token, bool IsSuccess);
    public class LoginHandler(ApplicationDbContext context )
        : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {

            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == command.UserName, cancellationToken);
            if (user != null)
            {
                if (!user.IsActive)
                {
                    throw new PasswordWrongException("Account has been locked");
                }

                bool IsSuccess = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);

                if (IsSuccess)
                {
                    var token = await GenerateJwtToken(user, command.Password);
                    return new LoginResult(token, true);
                }
                else
                {
                    user.FailedLoginAttempt += 1;
                    if (user.FailedLoginAttempt == 5)
                    {
                        user.IsActive = false;
                    }

                    throw new PasswordWrongException("Password is incorrect");
                }
            }
            else
            {
                throw new UserNotFoundException(command.UserName);
            }

        }

        public async Task<TokenModel> GenerateJwtToken(User user, string password)
        {
                

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHotelwebsite14"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", user.UserId.ToString()), // Claim userid (giả sử `user.Id` là Guid)
                new Claim("username", user.UserName), // Claim thêm thông tin username
            };
            var token = new JwtSecurityToken(
                issuer: "https://localhost:5056",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var tokens = new TokenModel
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires_in = 30
            };

            Console.WriteLine(tokens.Access_token);
            return tokens;
        }
    }
}