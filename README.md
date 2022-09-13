# TDF SDK

The TDF C# SDK allows developers to easily create and manage encrypted [Trusted Data Format (TDF) objects](https://github.com/virtru/tdf-spec) and interact with [Key Access and Entity Attribute Services (KAS, EAS).](https://developer.virtru.com/docs/architecture)

## Installation

Unpack the archive file, and copy the files from the lib and include directory to your application's source directory.  

## Authentication

opentdf allows users to set authentication headers (OIDC bearer tokens, etc), and will use those for authenticating with
backing services - by design it *does not contain* authentication flow logic, for example to exchange client credentials
for OIDC bearer tokens, refresh expired tokens, etc - it expects callers to handle that.

For an example of how to wrap this library in auth provider flows, see `virtru-tdf-cpp`

### OIDC

1. Provide KAS url, user id, and set the use_oidc flag to true:

```C#
TDFClient client = new TDFClient(kas_url, user_id, True);
```

2. Provide the TDFClient instance with your valid, previously obtained/generated OIDC bearer token:

```C#
client.set_auth_header("eyJhbGciOiJSUzI1NiIsInR5cCIg...");
```

### Legacy EAS

Two different methods:

1. Provide EAS url and user id (user should be registered on EAS):

```C#
TDFClient client = new TDFClient(eas_url, user_id);
```

2. Provide EAS url, user id, client key (absolute file path), client cetificate (absolute file path) and root CA (absolute file path)

```C#
TDFClient client = new TDFClient(eas_url, user_id, filepath_client_key, filepath_client_cert, filepath_rootCA);
```

## Create Encrypted TDF Object (minimal example)

```C#
NanoTDFClient client = new NanoTDFClient(eas_url, user_id);

client.encryptFile(unprotected_file, protected_file);
```

[Terms of Service](https://www.virtru.com/terms-of-service/)
[Privacy Policy](https://www.virtru.com/privacy-policy/)
