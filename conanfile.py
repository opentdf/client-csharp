from conans import ConanFile, CMake

class clientCsharpConan(ConanFile):
    generators = "cmake"
    settings = "os", "compiler", "build_type", "arch"

    def requirements(self):
        self.requires("opentdf-client/1.1.5@")

    def build(self):
        cmake = CMake(self)
        cmake.configure()
        cmake.build()
