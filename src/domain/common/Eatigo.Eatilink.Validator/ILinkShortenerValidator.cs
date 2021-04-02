using System;
using System.Collections.Generic;
using System.Text;
using ApplicationException = Eatigo.Eatilink.Common.Exceptions.ApplicationException;

namespace Eatigo.Eatilink.Validator
{
    public interface ILinkShortenerValidator
    {
        (int, ApplicationException) PayloadValidator(string accessToken, string originalUrl);
    }
}
