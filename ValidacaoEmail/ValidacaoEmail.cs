using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ValidacaoEmailBench
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net70, baseline: true)]
    public class ValidacaoMail
    {
        private static Regex _validateEmailRegex = new Regex(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$", RegexOptions.Compiled);

        // 3
        [Benchmark(Baseline = true)]
        public bool ChecaEmail_Regex()
        {
            // RFC 2822  compliant regex
            var validateEmailRegex = new Regex(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$");

            return validateEmailRegex.IsMatch("marcio.kgr@gmail.com");
        }

        [Benchmark()]
        public bool ChecaEmail_Regex_Compiled()
        {
            // RFC 2822  compliant regex
            var validateEmailRegex = new Regex(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$", RegexOptions.Compiled);

            return validateEmailRegex.IsMatch("marcio.kgr@gmail.com");
        }

        // 1
        [Benchmark()]
        public bool ChecaEmail_Regex_Compiled_Static()
        {
            return _validateEmailRegex.IsMatch("marcio.kgr@gmail.com");
        }

        // 2
        [Benchmark()]
        // RFC 2822 compliant regex
        public bool ChecaEmail_MailAddress() =>
            MailAddress.TryCreate("marcio.kgr@gmail.com", out var _);

    }
}
