using DnsClient;

namespace UserService.Application.Common.Services.Email;

public class EmailVerifier
{
    public static bool IsValidDomain(string email)
    {
        try
        {
            var domain = email.Split('@')[1];
            var lookup = new LookupClient();
            var result = lookup.Query(domain, QueryType.MX);

            return result.Answers.MxRecords().Any();
        }
        catch (Exception)
        {
            return false;
        }
    }
}
