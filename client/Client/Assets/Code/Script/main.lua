import "UnityEngine"
import "UnityEngine.UI"
import "UnityEngine.EventSystems"

function DoRequire()
    for name, v in pairs(package.loaded) do
        package.loaded[name] = nil
    end

    local ret, msg = pcall(require, "requires")
    if not ret then
        Debug.LogError(string.format("critial error! requires failed: %s", msg))
    end
    
    return ret
end

function main(bDebug, nIndex)
	if DoRequire() then
		print("SLua DoRequire Successed", enet, cmsgpack, resmng.SYSTEM_LOCAL)

		OneTimeInitScene()
	end
	
	return true
end

function OneTimeInitScene()
	RootContext.GetSingleton():Startup()
end

function AppExit()
	RootContext.GetSingleton():Shutdown()
end