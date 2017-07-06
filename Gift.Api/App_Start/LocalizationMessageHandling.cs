using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Gift.Api.Models;
using Gift.Framework.Extensions;

namespace Gift.Api
{
    public class LocalizationMessageHandler: DelegatingHandler {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            SetCulture(requestMessage);
            return await base.SendAsync(requestMessage, cancellationToken);
        }

        private void SetCulture(HttpRequestMessage requestMessage)
        {
            var supportedLanguageList = Enum<SupportedLanguageType>.ToListItem().ToList();
            var defaultCulture = SupportedLanguageType.English.GetEnumDescription<DisplayAttribute>().Name;

            foreach (var acceptLanguage in requestMessage.Headers.AcceptLanguage)
            {
                if (supportedLanguageList.Any(x => x.Contains(acceptLanguage.Value)))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(acceptLanguage.Value);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(acceptLanguage.Value);
                } else {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultCulture);
                }
            }
        }
    }
}