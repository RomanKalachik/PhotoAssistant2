
include_directories (${EXTRA_INCDIR} ${GTHREAD_INCLUDE_DIRS} ${GOBJECT_INCLUDE_DIRS} ${GLIB2_INCLUDE_DIRS}
    ${GLIBMM_INCLUDE_DIRS} ${IPTCDATA_INCLUDE_DIRS} ${LCMS_INCLUDE_DIRS} ${EXPAT_INCLUDE_DIRS} ${FFTW3F_INCLUDE_DIRS}
    ${GTKMM_INCLUDE_DIRS} ${GTK_INCLUDE_DIRS})
link_directories (
"${PROJECT_SOURCE_DIR}/rtexif" 
${EXTRA_LIBDIR} 
${GTHREAD_LIBRARY_DIRS} 
${GOBJECT_LIBRARY_DIRS} 
${GLIB2_LIBRARY_DIRS} 
${GLIBMM_LIBRARY_DIRS} 
${IPTCDATA_LIBRARY_DIRS} 
${LCMS_LIBRARY_DIRS} 
${EXPAT_LIBRARY_DIRS} 
${FFTW3F_LIBRARY_DIRS}
. 
${GTK_LIBRARY_DIRS} 
${GTKMM_LIBRARY_DIRS} 
${GIO_LIBRARY_DIRS} 
${GIOMM_LIBRARY_DIRS})

set (CAMCONSTSFILE "camconst.json")
set (libName "PhotoAssistantImageProcessing")

set (RTENGINESOURCEFILES wrappers_wrap.cxx safegtk.cc colortemp.cc curves.cc flatcurves.cc diagonalcurves.cc dcraw.cc iccstore.cc color.cc
    dfmanager.cc ffmanager.cc rawimage.cc image8.cc image16.cc imagefloat.cc imagedata.cc imageio.cc improcfun.cc init.cc dcrop.cc
    loadinitial.cc procparams.cc rawimagesource.cc demosaic_algos.cc shmap.cc simpleprocess.cc refreshmap.cc
    fast_demo.cc amaze_demosaic_RT.cc CA_correct_RT.cc cfa_linedn_RT.cc green_equil_RT.cc hilite_recon.cc expo_before_b.cc
    stdimagesource.cc myfile.cc iccjpeg.cc hlmultipliers.cc improccoordinator.cc editbuffer.cc
    processingjob.cc rtthumbnail.cc utils.cc labimage.cc slicer.cc cieimage.cc
    iplab2rgb.cc ipsharpen.cc iptransform.cc ipresize.cc ipvibrance.cc
    imagedimensions.cc jpeg_memsrc.cc jdatasrc.cc iimage.cc
    EdgePreservingDecomposition.cc cplx_wavelet_dec.cc FTblockDN.cc
    PF_correct_RT.cc previewimage.cc ipwavelet.cc
    dirpyr_equalizer.cc
    calc_distort.cc lcp.cc dcp.cc
    cJSON.c camconst.cc
    klt/convolve.cc klt/error.cc klt/klt.cc klt/klt_util.cc klt/pnmio.cc klt/pyramid.cc klt/selectGoodFeatures.cc
    klt/storeFeatures.cc klt/trackFeatures.cc klt/writeFeatures.cc
    clutstore.cc
    ciecam02.cc
    ../rtgui/options.cc
    ../rtgui/profilestore.cc)
#ADD_DEFINITIONS("-DSWIGEXPORT\"=__declspec(dllexport)\"")
#ADD_DEFINITIONS(-DSWIGSTDCALL)

include_directories (BEFORE "${CMAKE_CURRENT_BINARY_DIR}")
add_library (${libName} SHARED ${RTENGINESOURCEFILES})
set_target_properties (${libName} PROPERTIES COMPILE_FLAGS "${RTENGINE_CXX_FLAGS}")

target_link_libraries (
${libName} 
rtexif 
${EXTRA_LIB} 
${GOBJECT_LIBRARIES} 
${GTHREAD_LIBRARIES}
${GLIB2_LIBRARIES} 
${GLIBMM_LIBRARIES} 
${LCMS_LIBRARIES} 
${EXPAT_LIBRARIES} 
${FFTW3F_LIBRARIES} 
${IPTCDATA_LIBRARIES}
${JPEG_LIBRARIES} 
${PNG_LIBRARIES} 
${TIFF_LIBRARIES} 
${ZLIB_LIBRARIES}
${GTK_LIBRARIES} 
${GTKMM_LIBRARIES} 
${GIO_LIBRARIES} 
${GIOMM_LIBRARIES} 
${LCMS_LIBRARIES} 
${EXPAT_LIBRARIES}
${CANBERRA-GTK_LIBRARIES})

install (FILES lib${libName}.dll DESTINATION "${DATADIR}" PERMISSIONS OWNER_WRITE OWNER_READ GROUP_READ WORLD_READ)
