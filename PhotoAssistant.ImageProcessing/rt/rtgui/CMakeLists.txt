
set (BASESOURCEFILES
	rtimage.cc guiutils.cc options.cc)
#    editwindow.cc)
#    batchtoolpanelcoord.cc paramsedited.cc cropwindow.cc previewhandler.cc previewwindow.cc navigator.cc indclippedpanel.cc previewmodepanel.cc filterpanel.cc
#    exportpanel.cc cursormanager.cc rtwindow.cc renamedlg.cc recentbrowser.cc placesbrowser.cc filepanel.cc editorpanel.cc batchqueuepanel.cc
#    ilabel.cc thumbbrowserbase.cc adjuster.cc filebrowserentry.cc filebrowser.cc filethumbnailbuttonset.cc
#    cachemanager.cc cacheimagedata.cc shcselector.cc perspective.cc thresholdselector.cc thresholdadjuster.cc
#    clipboard.cc thumbimageupdater.cc bqentryupdater.cc lensgeom.cc coloredbar.cc edit.cc coordinateadjuster.cc
#    coarsepanel.cc cacorrection.cc  chmixer.cc blackwhite.cc
#    resize.cc icmpanel.cc crop.cc shadowshighlights.cc
#    impulsedenoise.cc dirpyrdenoise.cc epd.cc
#    exifpanel.cc toolpanel.cc lensprofile.cc
#    sharpening.cc vibrance.cc rgbcurves.cc  colortoning.cc
#    whitebalance.cc   vignetting.cc gradient.cc pcvignette.cc rotate.cc distortion.cc
#    crophandler.cc dirbrowser.cc
#    curveeditor.cc curveeditorgroup.cc diagonalcurveeditorsubgroup.cc flatcurveeditorsubgroup.cc
#    filecatalog.cc extprog.cc
#    previewloader.cc rtimage.cc inspector.cc
#    histogrampanel.cc history.cc  imagearea.cc
#    imageareapanel.cc iptcpanel.cc labcurve.cc main.cc
#    multilangmgr.cc mycurve.cc myflatcurve.cc mydiagonalcurve.cc options.cc
#    preferences.cc profilepanel.cc saveasdlg.cc
#    saveformatpanel.cc soundman.cc splash.cc
#    thumbnail.cc tonecurve.cc toolbar.cc
#    guiutils.cc zoompanel.cc toolpanelcoord.cc
#    thumbbrowserentrybase.cc batchqueueentry.cc
#    batchqueue.cc lwbutton.cc lwbuttonset.cc
#    batchqueuebuttonset.cc browserfilter.cc exiffiltersettings.cc
#    profilestore.cc partialpastedlg.cc
#    sensorbayer.cc sensorxtrans.cc preprocess.cc bayerpreprocess.cc bayerprocess.cc bayerrawexposure.cc xtransprocess.cc xtransrawexposure.cc
#    darkframe.cc flatfield.cc rawcacorrection.cc rawexposure.cc wavelet.cc
#    dirpyrequalizer.cc hsvequalizer.cc defringe.cc
#    popupcommon.cc popupbutton.cc popuptogglebutton.cc sharpenedge.cc sharpenmicro.cc colorappearance.cc
#    filmsimulation.cc prsharpening.cc)

include_directories (BEFORE "${CMAKE_CURRENT_BINARY_DIR}")

#    set (EXTRA_SRC windirmonitor.cc myicon.rc)
#    set (EXTRA_LIB_RTGUI winmm)
    include_directories (${EXTRA_INCDIR} ${GLIB2_INCLUDE_DIRS} ${GLIBMM_INCLUDE_DIRS}
        ${GTK_INCLUDE_DIRS} ${GTKMM_INCLUDE_DIRS} ${GIO_INCLUDE_DIRS} ${GIOMM_INCLUDE_DIRS})
    link_directories (. "${PROJECT_SOURCE_DIR}/rtexif" ${EXTRA_LIBDIR} ${GLIB2_LIBRARY_DIRS} ${GLIBMM_LIBRARY_DIRS}
        ${GTK_LIBRARY_DIRS} ${GTKMM_LIBRARY_DIRS} ${GIO_LIBRARY_DIRS} ${GIOMM_LIBRARY_DIRS})
    #set_target_properties (rth PROPERTIES LINK_FLAGS "-mwindows")
configure_file ("${CMAKE_CURRENT_SOURCE_DIR}/config.h.in" "${CMAKE_CURRENT_BINARY_DIR}/config.h")

#add_executable (rth ${EXTRA_SRC} ${BASESOURCEFILES})
add_library (rth SHARED ${BASESOURCEFILES})
add_dependencies (rth AboutFile)

set_target_properties (rth PROPERTIES COMPILE_FLAGS "${CMAKE_CXX_FLAGS}" OUTPUT_NAME rawtherapee)
target_link_libraries (rth rtengine ${JPEG_LIBRARIES} ${PNG_LIBRARIES} ${ZLIB_LIBRARIES} ${TIFF_LIBRARIES} ${GOBJECT_LIBRARIES} ${GTHREAD_LIBRARIES}
   ${GLIB2_LIBRARIES} ${GLIBMM_LIBRARIES} ${GTK_LIBRARIES} ${GTKMM_LIBRARIES} ${GIO_LIBRARIES} ${GIOMM_LIBRARIES} ${LCMS_LIBRARIES} ${EXPAT_LIBRARIES}
   ${FFTW3F_LIBRARIES} ${IPTCDATA_LIBRARIES} ${CANBERRA-GTK_LIBRARIES})
install (TARGETS rth DESTINATION ${BINDIR})

