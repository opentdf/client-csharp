/* virtru.i */
%module virtru

%include "std_string.i" 
%include "std_vector.i"

#define SWIG 1
#define SWIG_CSHARP 1

%rename(operatorEqual) operator=;

%{
#include "tdf_constants.h"
#include "oidc_credentials.h"
#include "tdf_client_base.h"
#include "tdf_client.h"
#include "nanotdf_client.h"
%}

%include "tdf_constants.h"
%include "oidc_credentials.h"
%include "tdf_client_base.h"
%include "tdf_client.h"
%include "nanotdf_client.h"
