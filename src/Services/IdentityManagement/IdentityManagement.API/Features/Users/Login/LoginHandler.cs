using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace IdentityManagement.API.Features.Users.Login
{
    public record LoginCommand(string UserName, string Password) : ICommand<LoginResult>;
    public record LoginResult(TokenModel Token, bool IsSuccess);
    public class LoginHandler(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == command.UserName, cancellationToken);
            if (user != null)
            {
                if (!user.IsActive)
                {
                    throw new PasswordWrongException("Tài khoản bị khóa");
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
                    if(user.FailedLoginAttempt == 5)
                    {
                        user.IsActive = false;
                    }

                    throw new PasswordWrongException("Password sai");
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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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


            //var client = httpClientFactory.CreateClient();
            //// Gửi request lấy token đến IdentityServer
            //var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:5056"); // Thay URL bằng URL của IdentityServer của bạn
            //if (discoveryDocument.IsError)
            //{
            //    throw new IdentityServerNotFoundException("IdentityServer không thể được tìm thấy.");
            //}

            //// Yêu cầu token
            //var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            //{
            //    Address = discoveryDocument.TokenEndpoint,
            //    ClientId = "webapp_client",  // Đặt ClientId
            //    ClientSecret = "secret",  // Đặt Client Secret đã băm
            //    Scope = "openid profile hotelmanagementAPI",  // Các scope được phép
            //    UserName = user.UserName,  // Username người dùng gửi
            //    Password = password, // Password người dùng gửi
            //});

            //// Kiểm tra xem yêu cầu token có thành công hay không
            //if (tokenResponse.IsError)
            //{
            //    throw new Exception($"Lỗi khi lấy token: {tokenResponse.Error}");
            //}

            //return new TokenModel
            //{
            //    Access_token = tokenResponse.AccessToken,
            //    Expires_in = tokenResponse.ExpiresIn
            //};
        }

    }
}
