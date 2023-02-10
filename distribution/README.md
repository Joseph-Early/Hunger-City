# Distribution

## Schema for `version.txt`
The version file only checks if the first-line is the same as the one downloaded with the binary, and if it is, it will not download the binary again, else it will download the new binary.

This directory contains the following files:
- `version.txt`: The latest version of binary.
- `linux-amd64.tar.gz`: The binary for Linux x86_64.
- `windows-amd64.zip`: The binary for Windows x86_64.
- `webgl.zip`: The binary for WebGL.