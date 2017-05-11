call wrappersGenerator.bat
set PATH=%PATH%;C:\msys64\mingw32\bin
SET PKG_CONFIG_PATH=/c/msys64/mingw32/lib/pkgconfig
cmake -D CMAKE_BUILD_TYPE=Debug -D MINGW_INSTALL_PREFIX=/c/msys64/mingw32 -D MAKE=make -D EXEC=sh -D CMAKE_MAKE_PROGRAM=mingw32-make.exe -G "MinGW Makefiles" -C rt\Win32CMakeOptions-MK1.txt  . 