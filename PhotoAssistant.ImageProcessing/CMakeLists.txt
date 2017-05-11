#set(BUILD_SHARED_LIBS TRUE)
set(CMAKE_VERBOSE_MAKEFILE ON)
set(INSTALL install)
if (WIN32)
  cmake_minimum_required(VERSION 2.8.4)
  cmake_policy(SET CMP0015 OLD)
else (WIN32)
  cmake_minimum_required(VERSION 2.6)
endif (WIN32)

PROJECT(RawTherapee)
include(ExternalProject)

set(TARGET_ARCH "x86_64-w64-mingw32" CACHE STRING "The target architecture for this toolchain.")
string(REGEX MATCH "^[^-]+" TARGET_CPU ${TARGET_ARCH})

execute_process(COMMAND ${CMAKE_C_COMPILER} -dumpmachine OUTPUT_VARIABLE HOST_ARCH)
string(REGEX MATCH "^[^-]+" HOST_CPU ${HOST_ARCH})

if(TARGET_CPU MATCHES "x86_64")
    set(TARGET_BITS 64)
else()
    set(TARGET_BITS 32)
endif()

configure_file(${CMAKE_CURRENT_SOURCE_DIR}/toolchain.cmake.in ${CMAKE_CURRENT_BINARY_DIR}/toolchain.cmake @ONLY)
set(TOOLCHAIN_FILE ${CMAKE_CURRENT_BINARY_DIR}/toolchain.cmake)

add_subdirectory(rt)