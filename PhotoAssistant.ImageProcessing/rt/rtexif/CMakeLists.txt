include_directories (					${EXTRA_INCDIR} ${GLIB2_INCLUDE_DIRS} ${GLIBMM_INCLUDE_DIRS} ${GTK_INCLUDE_DIRS} ${GTKMM_INCLUDE_DIRS})
link_directories (. "${PROJECT_SOURCE_DIR}/rtexif" 	${EXTRA_LIBDIR} ${GLIB2_LIBRARY_DIRS} ${GLIBMM_LIBRARY_DIRS} ${GTK_LIBRARY_DIRS} ${GTKMM_LIBRARY_DIRS})
include_directories (BEFORE "${CMAKE_CURRENT_BINARY_DIR}")

get_property(dirs DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR} PROPERTY LINK_DIRECTORIES)
foreach(dir ${dirs})
  message(STATUS "dir='${dir}'")
endforeach()



add_library (rtexif rtexif.cc stdattribs.cc nikonattribs.cc canonattribs.cc pentaxattribs.cc fujiattribs.cc sonyminoltaattribs.cc olympusattribs.cc kodakattribs.cc)
set_target_properties (rtexif PROPERTIES COMPILE_FLAGS " -ffast-math -fexpensive-optimizations")


IF (BUILD_SHARED_LIBS)
    INSTALL(TARGETS rtexif DESTINATION ${LIBDIR})
ENDIF (BUILD_SHARED_LIBS)
