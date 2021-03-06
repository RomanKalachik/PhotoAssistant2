cmake_minimum_required(VERSION 2.8.4)
cmake_policy(SET CMP0015 OLD)
PROJECT(RawTherapee)

# the default target is 'Debug'
if (CMAKE_BUILD_TYPE STREQUAL "")
  set (CMAKE_BUILD_TYPE Debug CACHE STRING "One of: None Debug Release RelWithDebInfo MinSizeRel." FORCE)
endif ()

string (TOUPPER ${CMAKE_BUILD_TYPE} UPPER_CMAKE_BUILD_TYPE)

if (UPPER_CMAKE_BUILD_TYPE STREQUAL "DEBUG")
  add_definitions (-D_DEBUG)
else ()
  add_definitions (-DNDEBUG)
  add_definitions (-D_DNDEBUG)
endif ()

message(STATUS "CMAKE_BUILD_TYPE: ${CMAKE_BUILD_TYPE}")

set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} "${CMAKE_SOURCE_DIR}/cmake/modules/")

# Examples: "" = ~/.config/RawTherapee, "latesttag" = ~/.config/RawTherapee4.2, "_testing" = ~/.config/RawTherapee_testing
set (CACHE_NAME_SUFFIX "" CACHE STRING "RawTherapee's cache folder suffix")

# By default, we don't use specific processor target, so  PROC_TARGET_NUMBER is set to 0. If can specify other values to select specific
# processor targets, which list can be found in ProcessorTargets.cmake.
set (PROC_TARGET_NUMBER 0 CACHE STRING "Selected target processor from the list above (taken from ProcessorTargets.cmake)")

# The following line set special compilation flags for RTEngine, and will be added to CMAKE_CXX_FLAGS
set (RTENGINE_CXX_FLAGS "" CACHE STRING "Special compilation flags for RTEngine")

#loading the processor targets list
include (ProcessorTargets.cmake)
set (PROC_LABEL "undefined" CACHE STRING "Target processor label, unused if PROC_TARGET_NUMBER = 0 or 2")
set (PROC_FLAGS "" CACHE STRING "Target processor related build/link flags")
if ((NOT(PROC_TARGET_NUMBER EQUAL 0)) AND (NOT(PROC_TARGET_NUMBER EQUAL 2)))
	set (PROC_LABEL ${PROC_TARGET_${PROC_TARGET_NUMBER}_LABEL})
endif ()
if (NOT(PROC_TARGET_NUMBER EQUAL 0))
	set (PROC_FLAGS ${PROC_TARGET_${PROC_TARGET_NUMBER}_FLAGS})
endif ()

# if it exists, the PROC_FORCED_LABEL value is loaded in PROC_LABEL to override the one of ProcessorTargets
if (DEFINED PROC_FORCED_LABEL)
  set (PROC_LABEL ${PROC_FORCED_LABEL})
endif (DEFINED PROC_FORCED_LABEL)

# adding the proc flags to the build flags
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${PROC_FLAGS}")
set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${PROC_FLAGS}")
set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} ${PROC_FLAGS}")

#stop compile on typos like std:swap (missing colon will be detected as unused label)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Werror=unused-label")

set(CMAKE_PREFIX_PATH $ENV{MINGW_BASEPATH} $ENV{GTKMM_BASEPATH} CACHE STRING "Additional search paths")
option(USE_EXPERIMENTAL_LANG_VERSIONS "Build RT with -std=c++0x" OFF)
option (BUILD_SHARED "Build rawtherapee with shared libraries" OFF)
option (WITH_BZIP "Build with Bzip2 support" ON)
option (WITH_MYFILE_MMAP "Build using memory mapped file" ON)
option (OPTION_OMP "Build with OpenMP support" ON)
option (PROTECT_VECTORS "Protect critical vectors by custom R/W Mutex, recommanded even if your std::vector is thread safe" ON)
option (STRICT_MUTEX "True (recommended): MyMutex will behave like POSIX Mutex; False: MyMutex will behave like POSIX RecMutex; Note: forced to ON for Debug builds" ON)
option (TRACE_MYRWMUTEX "Trace RT's custom R/W Mutex (Debug builds only); redirecting std::out to a file is strongly recommended!" OFF)
option (AUTO_GDK_FLUSH "Use gdk_flush on all gdk_thread_leave other than the GUI thread; set it ON if you experience X Server warning/errors" OFF)

set (BUILD_BUNDLE ON FORCE)

if (NOT DEFINED BUNDLE_BASE_INSTALL_DIR)
    set (BUNDLE_BASE_INSTALL_DIR "${CMAKE_BINARY_DIR}/${CMAKE_BUILD_TYPE}")
endif (NOT DEFINED BUNDLE_BASE_INSTALL_DIR)

if (BUILD_BUNDLE)
    set (BINDIR .)
    set (CMAKE_INSTALL_PREFIX "${BUNDLE_BASE_INSTALL_DIR}")
endif (BUILD_BUNDLE)

if (NOT DEFINED BINDIR)
    set (BINDIR "${CMAKE_INSTALL_PREFIX}/bin")
endif (NOT DEFINED BINDIR)

if (NOT DEFINED DATADIR)
    if (BUILD_BUNDLE)
        set (DATADIR .)
    else (BUILD_BUNDLE)
        set (DATADIR "${CMAKE_INSTALL_PREFIX}/share/rawtherapee")
    endif (BUILD_BUNDLE)
endif (NOT DEFINED DATADIR)

if (NOT DEFINED LIBDIR)
    if (BUILD_BUNDLE)
        set (LIBDIR .)
    else (BUILD_BUNDLE)
        # Respect CMAKE_INSTALL_LIBDIR if set
        if (DEFINED CMAKE_INSTALL_LIBDIR)
	    if (IS_ABSOLUTE "${LIBDIR}")
            	set (LIBDIR "${CMAKE_INSTALL_LIBDIR}")
	    else (IS_ABSOLUTE "${LIBDIR}")
		set (LIBDIR "${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_LIBDIR}")
	    endif (IS_ABSOLUTE "${LIBDIR}")
        else (DEFINED CMAKE_INSTALL_LIBDIR)
            set (LIBDIR "${CMAKE_INSTALL_PREFIX}/lib")
        endif (DEFINED CMAKE_INSTALL_LIBDIR)
    endif (BUILD_BUNDLE)
endif (NOT DEFINED LIBDIR)

if (NOT DEFINED DOCDIR)
    if (BUILD_BUNDLE)
        set (DOCDIR ./doc)
    else (BUILD_BUNDLE)
        set (DOCDIR "${CMAKE_INSTALL_PREFIX}/share/doc/rawtherapee")
    endif (BUILD_BUNDLE)
endif (NOT DEFINED DOCDIR)

# non-bundle builds has to use absolute paths
if (NOT BUILD_BUNDLE AND
    NOT (IS_ABSOLUTE "${BINDIR}" AND IS_ABSOLUTE "${DATADIR}" AND IS_ABSOLUTE "${LIBDIR}" AND
    IS_ABSOLUTE "${DOCDIR}" AND IS_ABSOLUTE "${CREDITSDIR}" AND IS_ABSOLUTE "${LICENCEDIR}"))
    message (FATAL_ERROR "The paths has to be absolute or use -DBUILD_BUNDLE=ON")
endif ()

# MyMutex
if (STRICT_MUTEX OR UPPER_CMAKE_BUILD_TYPE STREQUAL "DEBUG")
    add_definitions (-DSTRICT_MUTEX=1)
else (STRICT_MUTEX OR UPPER_CMAKE_BUILD_TYPE STREQUAL "DEBUG")
    add_definitions (-DSTRICT_MUTEX=0)
endif (STRICT_MUTEX OR UPPER_CMAKE_BUILD_TYPE STREQUAL "DEBUG")

# MyRWMutex
if (PROTECT_VECTORS)
    add_definitions (-DPROTECT_VECTORS=1)
else (PROTECT_VECTORS)
    add_definitions (-DPROTECT_VECTORS=0)
endif (PROTECT_VECTORS)
if (TRACE_MYRWMUTEX)
    # Note: it will be set to 0 for Debug builds (rtgui/guiutils.h)
    add_definitions (-DTRACE_MYRWMUTEX=1)
else (TRACE_MYRWMUTEX)
    add_definitions (-DTRACE_MYRWMUTEX=0)
endif (TRACE_MYRWMUTEX)

if (AUTO_GDK_FLUSH)
    add_definitions (-DAUTO_GDK_FLUSH=1)
else (AUTO_GDK_FLUSH)
    add_definitions (-DAUTO_GDK_FLUSH=0)
endif (AUTO_GDK_FLUSH)

# check for libraries
find_package(PkgConfig)
pkg_check_modules (GTK     REQUIRED gtk+-2.0>=2.12)
pkg_check_modules (GLIB2   REQUIRED glib-2.0>=2.16)
pkg_check_modules (GLIBMM  REQUIRED glibmm-2.4>=2.16)
pkg_check_modules (GTKMM   REQUIRED gtkmm-2.4>=2.22)
pkg_check_modules (GIO     REQUIRED gio-2.0>=2.16)
pkg_check_modules (GIOMM   REQUIRED giomm-2.4>=2.12)
pkg_check_modules (GTHREAD REQUIRED gthread-2.0>=2.16)
pkg_check_modules (GOBJECT REQUIRED gobject-2.0>=2.16)
pkg_check_modules (SIGC    REQUIRED sigc++-2.0)
# NOTE: The new mechanism has been tested with BUILD_SHARED = OFF
add_definitions (-DWIN32)
add_definitions (-D_WIN32)
add_definitions (-D__MINGW32__)
if (CMAKE_SIZEOF_VOID_P EQUAL 4)
	add_definitions (-DWINVER=0x0501)
endif (CMAKE_SIZEOF_VOID_P EQUAL 4)
set (EXTRA_LIB "-lws2_32 -lshlwapi")
pkg_check_modules (LCMS REQUIRED lcms2)
find_package (EXPAT REQUIRED expat>=2.0)
pkg_check_modules (FFTW3F REQUIRED fftw3f)
pkg_check_modules (IPTCDATA REQUIRED libiptcdata)
#pkg_check_modules(FFTW3 fftw3)
find_package (JPEG REQUIRED)
find_package (PNG REQUIRED)
find_package (TIFF REQUIRED)
find_package (ZLIB REQUIRED)

# link witz bzip
if (WITH_BZIP)
  find_package(BZip2)
  if (BZIP2_FOUND)
    add_definitions (-DBZIP_SUPPORT)
    set (EXTRA_INCDIR ${EXTRA_LIB} ${BZIP2_INCLUDE_DIR})
    set (EXTRA_LIB ${EXTRA_LIB} ${BZIP2_LIBRARIES})
  endif (BZIP2_FOUND)
endif (WITH_BZIP)

if (WITH_MYFILE_MMAP)
	add_definitions (-DMYFILE_MMAP)
endif (WITH_MYFILE_MMAP)

if (OPTION_OMP)
    find_package(OpenMP)
    if (OPENMP_FOUND)
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS} -Werror=unknown-pragmas")
    endif (OPENMP_FOUND)
endif (OPTION_OMP)

if(USE_EXPERIMENTAL_LANG_VERSIONS)
	SET (CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -std=gnu1x")
	SET (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=gnu++0x")
endif (USE_EXPERIMENTAL_LANG_VERSIONS)

# find out whether we are building out of source
get_filename_component(ABS_SOURCE_DIR "${PROJECT_SOURCE_DIR}" ABSOLUTE)
get_filename_component(ABS_BINARY_DIR "${CMAKE_BINARY_DIR}" ABSOLUTE)
set (OUT_OF_SOURCE_BUILD TRUE)
if (ABS_SOURCE_DIR STREQUAL ABS_BINARY_DIR)
    set (OUT_OF_SOURCE_BUILD FALSE)
    message (WARNING "You are performing an in-source build. This is discouraged. For an explanation and the advantages of out-of-source builds, please refer to http://www.cmake.org/Wiki/CMake_FAQ#What_is_an_.22out-of-source.22_build.3F")
endif ()

if (CMAKE_SIZEOF_VOID_P EQUAL 4)
  set(PROC_BIT_DEPTH 32 bits)
elseif (CMAKE_SIZEOF_VOID_P EQUAL 8)
  set(PROC_BIT_DEPTH 64 bits)
endif (CMAKE_SIZEOF_VOID_P EQUAL 4)

# Get comiler name and version
# only cmake > 2.8.7 knows CMAKE_*_COMPILER_VERSION
if (CMAKE_VERSION VERSION_GREATER 2.8.7)
  get_filename_component(COMPILER_INFO ${CMAKE_C_COMPILER} NAME_WE)
  set(COMPILER_INFO "${COMPILER_INFO} ${CMAKE_C_COMPILER_VERSION}")
else ()
  execute_process(COMMAND gcc -dumpversion OUTPUT_VARIABLE GCC_VERSION OUTPUT_STRIP_TRAILING_WHITESPACE)
  set(COMPILER_INFO "gcc ${GCC_VERSION}")
endif ()

# Get c++ and linker flags for rtengine (the gui's c++ flags may have less flags)
set(CXX_FLAGS "${CMAKE_CXX_FLAGS} ${CMAKE_CXX_FLAGS_${UPPER_CMAKE_BUILD_TYPE}} ${RTENGINE_CXX_FLAGS}")
set(LFLAGS "${CMAKE_EXE_LINKER_FLAGS} ${CMAKE_EXE_LINKER_FLAGS_${UPPER_CMAKE_BUILD_TYPE}}")

add_subdirectory (rtexif)
add_subdirectory (rtengine)
#add_subdirectory (rtgui)
#add_subdirectory (rtdata)