###########################################################
# CSharp bindings library
###########################################################
cmake_minimum_required (VERSION 3.8)

# enable verbose logging
set(CMAKE_VERBOSE_MAKEFILE ON)

# Set the project name
project(tdf-csharp)

set(CMAKE_CXX_STANDARD 17)

# Get this from VERSION file
file(READ ${PROJECT_SOURCE_DIR}/../VERSION CSHARP_SDK_BUILD_VERSION)
string(STRIP "${CSHARP_SDK_BUILD_VERSION}" CSHARP_SDK_BUILD_VERSION)
SET(PACKAGE_VERSION ${CSHARP_SDK_BUILD_VERSION})

set(TDFLIB "opentdf_static_combined")
set(TDF_LIB_DIR ${PROJECT_SOURCE_DIR}/../build/lib)

message(STATUS "Setting module suffix for ${CMAKE_SYSTEM_NAME}")
if(${CMAKE_SYSTEM_NAME} MATCHES "Windows")
    set(CSHARP_MODULE_SUFFIX ".dll")
    set(VBUILDTYPEDIR $ENV{VBUILDTYPE}/)
else()
  if(${CMAKE_SYSTEM_NAME} MATCHES "Darwin")
    set(CSHARP_MODULE_SUFFIX ".dylib")
  else()
    set(CSHARP_MODULE_SUFFIX ".so")
  endif()
endif()

message(STATUS "Module suffix ${CSHARP_MODULE_SUFFIX}")

# generate compile_commands.json
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)
set(SWIG_SOURCE_FILE "virtru.i")

find_package(SWIG REQUIRED)
include(UseSWIG)
set_source_files_properties(${SWIG_SOURCE_FILE} PROPERTIES CPLUSPLUS ON)

include_directories(
    ${CONAN_INCLUDE_DIRS}
)

link_directories(
    ${CONAN_LIB_DIRS}
)

link_libraries(
    ${CONAN_LIBS}
)

set(TDF_CSHARP_TARGET_DIR ${PROJECT_SOURCE_DIR}/../build/csharp-bindings)
set(TDF_CSHARP_INTERFACE_DIR ${PROJECT_SOURCE_DIR}/../build/csharp-bindings/source)

swig_add_library(
  ${PROJECT_NAME}
  LANGUAGE csharp
  OUTPUT_DIR ${TDF_CSHARP_INTERFACE_DIR}
  OUTFILE_DIR ${TDF_CSHARP_TARGET_DIR}
  SOURCES ${SWIG_SOURCE_FILE}
)

if(${CMAKE_SYSTEM_NAME} MATCHES "Darwin")
  set_target_properties(${PROJECT_NAME} PROPERTIES SUFFIX ${CSHARP_MODULE_SUFFIX})
endif()

set(TDF_CSHARP_NATIVE_LIB ${PROJECT_SOURCE_DIR}/../build/lib/${VBUILDTYPEDIR}tdf-csharp${CSHARP_MODULE_SUFFIX})
message(STATUS "TDF_CSHARP_NATIVE_LIB: ${TDF_CSHARP_NATIVE_LIB}")

############################################################
# Package for distribution
############################################################

# Create csharp distribution directory
set(TDF_CSHARP_INSTALL_LOCATION  ${PROJECT_SOURCE_DIR}/../tdf-lib-csharp)
install(DIRECTORY DESTINATION ${TDF_CSHARP_INSTALL_LOCATION})

set(TDF_CSHARP_EXAMPLES ${PROJECT_SOURCE_DIR}/../examples)

# TODO for some reason the following 'install' commands aren't doing anything
# Copy the native dynamic lib
install(FILES ${TDF_CSHARP_NATIVE_LIB} DESTINATION ${TDF_CSHARP_INSTALL_LOCATION}/lib)

if(${CMAKE_SYSTEM_NAME} MATCHES "Windows")
  # Also need matching interface .lib file to go with .dll file on windows
  set(TDF_CSHARP_NATIVE_LIB_INTERFACE ${PROJECT_SOURCE_DIR}/../build/lib/${VBUILDTYPEDIR}tdf-csharp.lib)
  message(STATUS "TDF_CSHARP_NATIVE_LIB_INTERFACE: ${TDF_CSHARP_NATIVE_LIB_INTERFACE}")
  install(FILES ${TDF_CSHARP_NATIVE_LIB_INTERFACE} DESTINATION ${TDF_CSHARP_INSTALL_LOCATION}/lib)
endif()

# Copy the interface files (note: trailing slash on source is there to copy files in dir instead of dir itself)
message(STATUS "TDF_CSHARP_INTERFACE_DIR: ${TDF_CSHARP_INTERFACE_DIR}")
install(DIRECTORY ${TDF_CSHARP_INTERFACE_DIR}/ DESTINATION ${TDF_CSHARP_INSTALL_LOCATION}/include FILES_MATCHING PATTERN "*.cs")

# Copy the sample dir
file(COPY ${TDF_CSHARP_EXAMPLES} DESTINATION ${TDF_CSHARP_INSTALL_LOCATION})

# Copy the README, VERSION, and LICENSE
CONFIGURE_FILE(${PROJECT_SOURCE_DIR}/../README.md
        ${TDF_CSHARP_INSTALL_LOCATION})

CONFIGURE_FILE(${PROJECT_SOURCE_DIR}/../VERSION
        ${TDF_CSHARP_INSTALL_LOCATION})

CONFIGURE_FILE(${PROJECT_SOURCE_DIR}/../LICENSE
        ${TDF_CSHARP_INSTALL_LOCATION})

