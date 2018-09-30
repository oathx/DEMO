module("resmng", package.seeall)

require("Config/common/define_common_resource")
require("Config/common/define")
require("Config/common/protocol")

require("Config/client/define_local")
require("Config/client/define_system")
require("Config/client/define_scene")
require("Config/client/define_hero")
require("Config/client/define_task")


-- require local language packget
require("Config/client/define_language")
if _LANGUAGE_CN then
require("Config/client/prop_lang_cn")
else
require("Config/client/prop_lang_en")
end

-- require game config file
require("Config/client/prop_local")
require("Config/client/prop_system")
require("Config/client/prop_scene")
require("Config/client/prop_hero")
require("Config/client/prop_task")

function LangText(nID, ...)
    local arg = {...}

    if type(nID) == "string" then
        id = resmng[nID]
    end

    if #arg ~= 0 then
        return string.format(resmng.propLang[nID], unpack(arg))
    end

    return resmng.propLang[nID]
end

if not resmng.propLang then
    error("Please define Unity macors _LANGUAGE_CN or _LANGUAGE_EN")
end

