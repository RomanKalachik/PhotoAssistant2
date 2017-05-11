%{
#include <glibmm/ustring.h>
#define SWIGSTDCALL __stdcall
#define SWIGEXPORT __declspec(dllexport)
%}
namespace Glib {
%naturalvar ustring;
class ustring;
// ustring
%typemap(ctype) ustring "char *"
%typemap(imtype) ustring "string"
%typemap(cstype) ustring "string"
%typemap(csdirectorin) ustring "$iminput"
%typemap(csdirectorout) ustring "$cscall"

%typemap(in, canthrow=1) ustring 
%{ if(!$input) {
     SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "null Glib::ustring", 0);
     return $null;
    } 
    $1.assign($input); %}

%typemap(directorout, canthrow=1) ustring 
%{ if (!$input) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "null Glib::ustring", 0);
    return $null;
   }
   $result.assign($input); %}


%typemap(directorin) ustring %{ $input = SWIG_csharp_string_callback($1.c_str()); %}
%typemap(out) ustring %{ $result = SWIG_csharp_string_callback($1.c_str()); %}


%typemap(csin) ustring "$csinput"
%typemap(csout, excode=SWIGEXCODE) ustring {
    string ret = $imcall;$excode
    return ret;
  }


%typemap(typecheck) ustring = char *;
%typemap(throws, canthrow=1) ustring
%{ SWIG_CSharpSetPendingException(SWIG_CSharpApplicationException, $1.c_str());
   return $null; %}

// const ustring &
%typemap(ctype) const ustring & "char *"
%typemap(imtype) const ustring & "string"
%typemap(cstype) const ustring & "string"

%typemap(csdirectorin) const ustring & "$iminput"
%typemap(csdirectorout) const ustring & "$cscall"

%typemap(in, canthrow=1) const ustring &
%{ if (!$input) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "null ustring", 0);
    return $null;
   }
   $*1_ltype $1_str($input);
   $1 = &$1_str; %}
%typemap(out, canthrow=1) const ustring & %{ $result = SWIG_csharp_string_callback($1->c_str()); %}

%typemap(csin) const ustring & "$csinput"
%typemap(csout, excode=SWIGEXCODE) const ustring & {
    string ret = $imcall;$excode
    return ret;
  }

%typemap(directorout, canthrow=1, warning=SWIGWARN_TYPEMAP_THREAD_UNSAFE_MSG) const ustring &
%{ if (!$input) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "null ustring", 0);
    return $null;
   }
   /* possible thread/reentrant code problem */
   static $*1_ltype $1_str;
   $1_str = $input;
   $result = &$1_str; %}

%typemap(directorin, canthrow=1) const ustring & %{ $input = SWIG_csharp_string_callback($1.c_str()); %}

%typemap(csvarin, excode=SWIGEXCODE2) const ustring & %{
    set {
      $imcall;$excode
    } %}
%typemap(csvarout, excode=SWIGEXCODE2) const ustring & %{
    get {
      string ret = $imcall;$excode
      return ret;
    } %}

%typemap(typecheck) const ustring & = char *;

%typemap(throws, canthrow=1) const ustring &
%{ SWIG_CSharpSetPendingException(SWIG_CSharpApplicationException, $1.c_str());
   return $null; %}
}