# Eatilink
Eatilink are shortened links. This can begin with the eati.go


# Token Authentication
For token authentication, in every request API have to send JWT as Bearer in header Authorization like:
## Authorization: Bearer {{AccessToken}}
## Security claims JSON Web Token (JWT)
## JOSE Header Algorithm HMAC SHA256 (Base64Url )
## JWS Payload Identity (Must Include user identity)

Eatilink API will receive bearer token as JWT and will decode JWT using a secret key and will make sure
that token is valid. In case of an expired or invalid JWT, Eatilink API will return an error with HTTP code
401 (Unauthorized).

# Shorten a Link
POST/api/v{version}/LinkShortener/shorten


# Configuration

## Serilog
Please specify the log path where application log will be saved. It’s a rolling file and we can set
the log level too.

## Database Settings
Contains MongoDB database credentials including connection string, collection name and
database name.

## JWT Token
Requests will be authorized by JsonWebToken secret. Eatilink api is also configurable with
token enable or disable. If we don’t want to authorize, please specify false it’s isEnabled
parameter.

## Memory Cache
Auto refreshing memory cache is in place and the time limit is also configurable.

