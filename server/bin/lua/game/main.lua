require("util")

function do_require()
    local ary_path = tools.string_split(package.path, ";")
    for k, v in pairs(ary_path) do
        print(k, v)
    end
    
    for name, v in pairs(package.loaded) do
        package.loaded[name] = nil
    end

    local serconf = arg[1]
	if serconf then
		dofile(serconf)
	end

    local reaload = arg[2]
    if reaload then
        local ret, msg = pcall(require, reaload)
        if not ret then
            error("critial error! requires failed: ", msg, reaload)
        end
    end
	
    return true
end

function main()
    if do_require() then
        local entity = Entity()
        if entity then
            local thread = uvcore.new_thread()
            if thread then

            end
            print(thread)
            local server = entity:AddComponent(GameApp)
            if server then
                server:CreateServer(Serconf.host, Serconf.port, Serconf.max)
            end
        end
    end
end