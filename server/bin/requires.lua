require("config")

uvcore = require("luv")
if not uvcore then
	error("can't open luv lib")
end

string.pack = string.pack or uvcore.bpack
string.unpack = string.unpack or uvcore.bunpack

require("core/h5server")