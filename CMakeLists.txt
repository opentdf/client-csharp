cmake_minimum_required (VERSION 3.8)
project(opentdf-csharp)

# enable verbose logging
set(CMAKE_VERBOSE_MAKEFILE ON)

include(${CMAKE_BINARY_DIR}/conanbuildinfo.cmake)
conan_basic_setup()

add_subdirectory(src)
