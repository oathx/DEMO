module("Protocol", package.seeall)

Server = {
	-- pack = {
	-- 	userName = ""
	-- 	password = ""
	-- }
	checkIn = "pack msg",
}

Client = {
	checkIn = "pack msg",
	errorCode = "int code"
}

print("--------------dump server protocol--------------")
for k, v in pairs(Server) do
	print(k, v)
end

print("--------------dump client protocol--------------")
for k, v in pairs(Client) do
	print(k, v)
end
	
function new()
	local protocol = {
	}
	setmetatable(protocol, {__index=Protocol})
	protocol:init()
	
	return protocol
end

function init(self)
	self.Server = Server
	self.Clinet = Client
end
