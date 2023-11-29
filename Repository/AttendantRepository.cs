using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using midterm_SE4458.Model;

namespace Repository
{
    public class AttendantRepository
    {
        private IConfiguration _configuration;
        public AttendantRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Attendant? CreateAttendant(SignUp modal)
        {
            using var context = new SelimContext();
            try
            {
                var user = new Attendant
                {
                    FirstName = modal.FirstName,
                    LastName = modal.LastName,
                    Username = modal.Username,
                    AttendantPassword = modal.Password
                };
                user = CreateToken(user);

                context.Attendants.Add(user);
                context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Attendant? GetAttendantByUsername(string username)
        {
            using var context = new SelimContext();
            try
            {
                var user = context.Attendants.Single(u => u.Username == username);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Attendant? GetAttendantByToken(string token)
        {
            using var context = new SelimContext();
            try
            {
                var user = context.Attendants.Single(u => u.Token == token);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Attendant? GetAttendantLogin(Login login)
        {
            using var context = new SelimContext();
            try
            {
                var user = context.Attendants.Single(u => u.Username == login.Username && u.AttendantPassword == login.Password);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private Attendant CreateToken(Attendant user)
        {
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.Username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = GetToken(authClaims);
            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return user;
        }
        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}