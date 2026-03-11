using Saml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  DB_Middleware 
{
    public class samlDyanmics
    {
        public static string Certificatedata()
        {
            //Minda CERTIFICATE
            string samlCertificate = @"-----BEGIN CERTIFICATE-----
MIIC8DCCAdigAwIBAgIQExKVqUsHfLBC8MHghjf56DANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQD
EylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yNDEyMDkxMjA4
NTdaFw0yNzEyMDkxMjA4NDFaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQg
U1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtcxeAK8LbS9X
Fa1WR0BZIdseP4aZjX/mxrWSyca1iSSQEbiTAmvknVNBDh75Pc1XTdKrO2yURKiIrPvfz2Mt1zrw
pEd52Qzc7KTaPMH/cK8qSk7UCISRWL0DdbubVPM7b12Zr+5BuLNgF6EFB1tb2Q6NGTe1zbbjP9tX
N2lNU5t5hxJlNDWT3wnk0ZfvREYw325qm5XDK6L2uxVLsuqL9+GzN4xTr1ghOnFlGaL3rlhhm6XL
6FMJM0iPv4fAtqhNG5tJamFjpbqaOBSEtaoTiiRljSddCelzupbQ6nEUI2BYCc6pRhrN6zhVDiJP
xOmn7BJHnkg4nMdcV052VzF0OQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCHfZnI5hh+FlDMrdkW
DIuIL1b3HvlEe7FLYqICT7n2/DUrcOyXUwjFtX+5cky4HoPBSQ0nR7L5tVDWf+Dru/MTsk1V+X6q
Ic7iOHem+qW1w2Pndt55dJXlK94f6aqpHL4LOAbiYPtZ72O391dICrL6Nt6E7Jge76kAVyPkwXy3
4iQNHTz3HjVX0Y/m3pi/NqS/zE/UgQ4yBCzkj455DGpQTu3WIEpX7fQPCsaQObn5VDb5JD4Z1xk1
DrfAYKs8O5O8sC6F8TihjpHouj+Nalh8mipmdCR/Y10HIdlWbQ16k+HoWJsMSCXHAV6qff5KtsST
QV/dv0DaaK9BaxbCmkXT
-----END CERTIFICATE-----";


            //Test CERTIFICATE
            //string samlCertificate = @"-----BEGIN CERTIFICATE-----
            //                MIIC8DCCAdigAwIBAgIQfW2/8idQWoJBwtdlQigz/jANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMTAyMDUwNzE4NTdaFw0yNDAyMDUwNzE4NTdaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqVLEU4XA/9GWr5aTEs6yO/Yf1SrnYaxSp8qPWWQ83pGrv3lwjhZ3PEfrwhn3RU79b603D1rV12k0ipxfRnZj0WoNPTMqtTz/QpUdFKPjhT/MVg0wdkldyfVJs1FsmgkZ+ZDzXaIXaQUcgeT8VpRWvPXcdvk2pA9rF6Q3olO1MySq7hvGNesQKNz3n5Opwc5L8j/jqosn8KzL805NmLAHCtQ115W7dZU+L9wRIaNG3DlqiY8BbtAK5WVQ9VeY7D4vbiO7Yq98tfbLzf3VPSSOQ5tlaGHAlWu9CLrdFYjmuULNUf975kiRjYu1k7CI/DHaVlgJQ1zGtFnGidgWFgN/SQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQAeumR4yNlWdQ7RTUVfOQ+EiF4ENDnGhBobD+T62kllJ1p8UeLLoX1+ZVYW32rhPAQ3sPB24mnACS8CH04/MvA8qv7wa3Fq5cM2hHxYO6P203I1JIEI2aZoN7Oiu+ufCNpSCXmoWRDNITVMRGfKqkfE+jXnCMbdh7yi7OOwLRuNV0+m23LYzehhaXVtPzjjvRGVJHj7+QSZoLKVKpnrNzBvRgJyB59DAYazpz6t0JUCFUMrgC7RfxMWeigYXn5eHPPBBRg/7SD/IUQgLk6EDHf72JjMZq915bWRV23prpAt/vD6/mnWKm8o4YvSFP5UUgCrZvlxjkBho63qFWsSRB9F
            //                -----END CERTIFICATE-----";

            return samlCertificate;
        }


        public static string TestCertificatedata()
        {
            //Minda CERTIFICATE
            string samlCertificate = @"-----BEGIN CERTIFICATE-----
MIIC8DCCAdigAwIBAgIQfbA8q6CaJ4ZKpUmRxPxkpjANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMzA1MDQxMzIyMzZaFw0yNjA1MDQxMzIyMzVaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsLOIXGrLD0IuCHIT441mBg1oYO7tezAY8OECa88/Uk2yL5ax+s+UfYcI8wRLxPY1y8ZK60KSTccUgknvCyvqt/abSASsCVUvgg1Pb9IBXEfJfoqdmN0FCGXi0gd1ws1f0QhpSmhi8pkHs3RzKJg/HPvQtGFFX7Buybl6Er8TBJdnIjeClM2u4l2NxoqDZQgLPl6y9r4Y5JYZvS6vqNzfyafSVmQTE8n3PFV2EsB5hRuUGC+JZMSjRTw5SYQXCEVSjTTtVcXdhOi+HEo0ffQXuB50HHSoQs2BmiH+xYuz39GFVUMvISSNstvvKP5MsVOrz0KwPHWjq0oFgILmtHxw4QIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCsJ30Z8NzxAzwZn2TNrhdrwm1TMy0FmNRG/agIP1Q4AcOOZ5uqVKZ3FxPqwjiB0MNyq+PBrVX1l/QlkkKU8El5awWuVxMJkIUKxY6H9cGPzp0B7BMYE3ObGWXqocqASDL34GWcL8q/T1UfYOaH4PhQDhB4OLUAsaUpDYZFtFPyYcOgecBq2hqMyn2obzFaQRmogQGhHZJtcEKQUqtj5/5gsqFw0JuGTSox+rv319YQkMkdWMyhMJUhNZkH8fuw7krznhxV7VDEXbnLNr4W985UQoGoNvoAxylponBTFgYVFhGmAL0hFrjEvhgMhg8D2p7MzzFjz9xxPP6XcpfgVobR
-----END CERTIFICATE-----";


            //Test CERTIFICATE
            //string samlCertificate = @"-----BEGIN CERTIFICATE-----
            //                MIIC8DCCAdigAwIBAgIQfW2/8idQWoJBwtdlQigz/jANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMTAyMDUwNzE4NTdaFw0yNDAyMDUwNzE4NTdaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqVLEU4XA/9GWr5aTEs6yO/Yf1SrnYaxSp8qPWWQ83pGrv3lwjhZ3PEfrwhn3RU79b603D1rV12k0ipxfRnZj0WoNPTMqtTz/QpUdFKPjhT/MVg0wdkldyfVJs1FsmgkZ+ZDzXaIXaQUcgeT8VpRWvPXcdvk2pA9rF6Q3olO1MySq7hvGNesQKNz3n5Opwc5L8j/jqosn8KzL805NmLAHCtQ115W7dZU+L9wRIaNG3DlqiY8BbtAK5WVQ9VeY7D4vbiO7Yq98tfbLzf3VPSSOQ5tlaGHAlWu9CLrdFYjmuULNUf975kiRjYu1k7CI/DHaVlgJQ1zGtFnGidgWFgN/SQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQAeumR4yNlWdQ7RTUVfOQ+EiF4ENDnGhBobD+T62kllJ1p8UeLLoX1+ZVYW32rhPAQ3sPB24mnACS8CH04/MvA8qv7wa3Fq5cM2hHxYO6P203I1JIEI2aZoN7Oiu+ufCNpSCXmoWRDNITVMRGfKqkfE+jXnCMbdh7yi7OOwLRuNV0+m23LYzehhaXVtPzjjvRGVJHj7+QSZoLKVKpnrNzBvRgJyB59DAYazpz6t0JUCFUMrgC7RfxMWeigYXn5eHPPBBRg/7SD/IUQgLk6EDHf72JjMZq915bWRV23prpAt/vD6/mnWKm8o4YvSFP5UUgCrZvlxjkBho63qFWsSRB9F
            //                -----END CERTIFICATE-----";

            return samlCertificate;
        }

        public static dynamic AzureAuthRequest()
        {
            //Minda
            dynamic request = new AuthRequest(
                       "https://Budget.sparkminda.in/",               //TODO: put your app's "entity ID" here
                       "https://Budget.sparkminda.in/Account/Logout"      //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
                       );

            //Local
            //dynamic request = new AuthRequest(
            //          "http://localhost:62453/",               //TODO: put your app's "entity ID" here
            //          "http://localhost:62453/Account/Logout"      //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
            //          );

            return request;
        }


    }

    public class AzureUrlData
    {
        //Local testing            
        //public static string samlLogin = "https://login.microsoftonline.com/bc7f042c-c734-4556-9861-93f2835fe283/saml2";
        //public static string samlLogout = "https://login.microsoftonline.com/bc7f042c-c734-4556-9861-93f2835fe283/saml2";
        //public static string samlIdentifier = "https://sts.windows.net/bc7f042c-c734-4556-9861-93f2835fe283/";

        //Minda 
        public static string samlLogin = "https://login.microsoftonline.com/0b9df38c-637f-4b17-b290-cd6eece5e15b/saml2";
        public static string samlLogout = "https://login.microsoftonline.com/common/wsfederation?wa=wsignout1.0";
        public static string samlIdentifier = "https://sts.windows.net/0b9df38c-637f-4b17-b290-cd6eece5e15b/";
    }
}