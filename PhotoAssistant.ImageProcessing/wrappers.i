%module libPhotoAssistantImageProcessing
%{
#include "rt_math.h"
#include "procparams.h"
#include "procevents.h"
#include "../rtexif/rtexif.h"
#include "rawmetadatalocation.h"
#include "iimage.h"
#include "utils.h"
#include "settings.h"
#include "LUT.h"
#include "rtengine.h"
#include "colortemp.h"
#include "coord2d.h"
#include "dcp.h"
#include "imagedata.h"
#include "image8.h"
#include "image16.h"
#include "imagefloat.h"
#include "imagesource.h"
#include "rawimagesource.h"
#include "previewimage.h"
using namespace std;
using namespace rtengine;
//using namespace rtexif;
//using namespace procparams;
%}

%ignore operator =;
%ignore operator=;
%ignore Mutex;
%ignore MyLock;
%ignore get2;
%ignore get4;
%ignore sset2;
%ignore sset4;
%ignore int_to_float;
%ignore Glib::Threads::Mutex::operator=;
%ignore initMakerNote;
%ignore handlerMutex;
%ignore access;
%ignore getStdImage;
%ignore getImage;
%ignore getSize;

%include "ustring.i"
%include "stl.i"
//%include "windows.i"
%include "rt_math.h"
%include "procparams.h"
%include "procevents.h"
%include "../rtexif/rtexif.h"
%include "rawmetadatalocation.h"
%include "iimage.h"
%include "utils.h"
%include "../rtgui/threadutils.h"
%include "../rtgui/options.h"
%include "settings.h"
%include "LUT.h"
%include "rtengine.h"
%include "colortemp.h"
%include "coord2d.h"
%include "dcp.h"
%include "imagedata.h"
%include "image8.h"
%include "image16.h"
%include "imagefloat.h"
%include "imagesource.h"
%include "rawimagesource.h"
%include "previewimage.h"
namespace Cairo
{

#define CAIRO_FORMAT_ARGB32 0
#define CAIRO_FORMAT_RGB24 1
#define CAIRO_FORMAT_A8 2
#define CAIRO_FORMAT_A1 3
#define CAIRO_FORMAT_RGB16_565 4
#define CAIRO_FORMAT_RGB30 5

	typedef enum
	{
    	FORMAT_ARGB32 = CAIRO_FORMAT_ARGB32,
    	FORMAT_RGB24 = CAIRO_FORMAT_RGB24,
    	FORMAT_A8 = CAIRO_FORMAT_A8,
    	FORMAT_A1 = CAIRO_FORMAT_A1,
    	FORMAT_RGB16_565 = CAIRO_FORMAT_RGB16_565
	} Format;
}