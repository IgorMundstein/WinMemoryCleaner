using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Validator
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates the code signing certificate
        /// </summary>
        public static bool IsCertificateValid()
        {
            try
            {
                X509Certificate2 certificate;

                try
                {
                    certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(Assembly.GetExecutingAssembly().Location));

                    if (certificate == null)
                        throw new UnauthorizedAccessException();
                }
                catch
                {
                    throw new UnauthorizedAccessException("The executable is not signed or the certificate could not be loaded.");
                }

                var thumbprint = certificate.Thumbprint != null ? certificate.Thumbprint.Replace(" ", "").ToUpperInvariant() : null;

                if (thumbprint == null)
                    throw new UnauthorizedAccessException("The certificate does not have a thumbprint.");

                if (!string.Equals(thumbprint, Constants.App.Certificate.Release.Thumbprint, StringComparison.OrdinalIgnoreCase) && 
                    !string.Equals(thumbprint, Constants.App.Certificate.Test.Thumbprint, StringComparison.OrdinalIgnoreCase))
                    throw new UnauthorizedAccessException("The certificate thumbprint does not match.");

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e, string.Format(Localizer.Culture, "{0} Error: {1}", Localizer.String.SecurityWarning, e.Message));

                return false;
            }
        }
    }
}
