using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

public class IAAsite : Script
{
	ScriptSettings IniSettings;
	bool BlipsActive;
	bool BlipsLR;
	int ProcessingZoneIAABlipColor;
	bool Initialized = false;

	//Coords of IAA Stuff
	Vector3 ServerBanksIAAEntryPos = new Vector3(2033.63f, 2942.24f, -62.90f);
	Vector3 ServerBanksIAAExitPos = new Vector3(2154.73f, 2921.04f, -81.08f);
	Vector3 ServerBanksIAASpawnPosIn = new Vector3(2154.73f, 2921.04f, -81.08f);
	Vector3 ServerBanksIAASpawnPosOut = new Vector3(2033.63f, 2942.24f, -61.90f);
	Vector3 ProcessingZoneIAAEntryPos = new Vector3(2521.03f, -414.84f, 93.13f);
	Vector3 ProcessingZoneIAAExitPos = new Vector3(2155.11f, 2920.93f, -62.90f);
	Vector3 ProcessingZoneIAASpawnPosIn = new Vector3(2155.11f, 2920.93f, -61.90f);
	Vector3 ProcessingZoneIAASpawnPosOut = new Vector3(2521.03f, -414.84f, 93.13f);


    public IAAsite()
    {
        IniSettings = ScriptSettings.Load("scripts\\IAAsite.ini");
        BlipsActive = IniSettings.GetValue<bool>("Site Configuration", "Blips Active", false);
        BlipsLR = IniSettings.GetValue<bool>("Site Configuration", "Blips Borders", false);
        ProcessingZoneIAABlipColor = IniSettings.GetValue<int>("Blip Colors", "ProcessingZoneIAA bc", 0);
        Tick += OnTick;
        IPL();

        if (BlipsActive)
        {

            Blip ProcessingZoneIAA = World.CreateBlip(new Vector3(2521.03f, -414.84f, 94.13f));
            Function.Call(Hash.SET_BLIP_SPRITE, ProcessingZoneIAA, 468);
            Function.Call(Hash.SET_BLIP_COLOUR, ProcessingZoneIAA, ProcessingZoneIAABlipColor);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "IAA Data Processing Zone");
            Function.Call((Hash)0xBC38B49BCB83BC9B, ProcessingZoneIAA);


            if (!BlipsLR)
            {

                Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, ProcessingZoneIAA, true);

            }
        }

    }

    void IPL()
    {

        //IAA Server Banks
        Function.Call(Hash.REQUEST_IPL, "xm_x17dlc_int_placement_interior_4_x17dlc_int_facility_milo_");

        //IAA Processing Zone A
        Function.Call(Hash.REQUEST_IPL, "xm_x17dlc_int_placement_interior_5_x17dlc_int_facility2_milo_");

    }

    void DisplayHelpText(string text)
    {
        InputArgument[] arguments = new InputArgument[] { "STRING" };
        Function.Call((Hash)0x8509B634FBE7DA11, arguments);
        InputArgument[] argumentArray2 = new InputArgument[] { text };
        Function.Call((Hash)0x6C188BE134E074AA, argumentArray2);
        InputArgument[] argumentArray3 = new InputArgument[] { 0, 0, 1, -1 };
        Function.Call((Hash)0x238FFE5C7B0498A6, argumentArray3);
    }


    void OnTick(object sender, EventArgs e)
    {
        Ped plrChar = Game.Player.Character;

        //IAA Server Banks
        if (plrChar != null & plrChar.IsAlive && plrChar.Position.DistanceTo(ServerBanksIAAEntryPos) < 2f)
        {
            Function.Call(Hash.DRAW_MARKER, 1, 2033.63f, 2942.24f, -62.90f, 0f, 0f, 0f, 0f, 0f, 0f, 0.6f, 0.6f, 0.5f, 240, 200, 80, 150, false, false, 2, false, false, false);
            DisplayHelpText("Press ~INPUT_CONTEXT~ to enter the IAA Server Banks.");
        }
        if (Game.IsControlJustPressed(2, GTA.Control.Context) && plrChar.Position.DistanceTo(ServerBanksIAAEntryPos) < 2f)
        {
            Game.FadeScreenOut(100);
            Script.Wait(1500);
            Game.Player.Character.Position = ServerBanksIAASpawnPosIn;
            Game.Player.Character.Heading = 269.39f;
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
            Script.Wait(1500);
            Game.FadeScreenIn(500);
        }
        if (plrChar != null & plrChar.IsAlive && plrChar.Position.DistanceTo(ServerBanksIAAExitPos) < 2f)
        {
            Function.Call(Hash.DRAW_MARKER, 1, 2154.73f, 2921.04f, -82.08f, 0f, 0f, 0f, 0f, 0f, 0f, 0.6f, 0.6f, 0.5f, 240, 200, 80, 150, false, false, 2, false, false, false);
            DisplayHelpText("Press ~INPUT_CONTEXT~ to use the elevator and exit the IAA Server Banks.");
        }
        if (Game.IsControlJustPressed(2, GTA.Control.Context) && plrChar.Position.DistanceTo(ServerBanksIAAExitPos) < 2f)
        {
            Game.Player.Character.FreezePosition = true;
            Game.FadeScreenOut(100);
            Script.Wait(1500);
            Game.Player.Character.Position = ServerBanksIAASpawnPosOut;
            Game.Player.Character.Heading = 267.66f;
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
            Game.Player.Character.FreezePosition = false;
            Script.Wait(1500);
            Game.FadeScreenIn(400);
        }

        //IAA Processing Zone A
        if (plrChar != null & plrChar.IsAlive && plrChar.Position.DistanceTo(ProcessingZoneIAAEntryPos) < 2f)
        {
            Function.Call(Hash.DRAW_MARKER, 1, 2521.03f, -414.84f, 93.12f, 0f, 0f, 0f, 0f, 0f, 0f, 0.6f, 0.6f, 0.5f, 240, 200, 80, 150, false, false, 2, false, false, false);
            DisplayHelpText("Press ~INPUT_CONTEXT~ to enter the IAA Data Processing Zone.");
        }
        if (Game.IsControlJustPressed(2, GTA.Control.Context) && plrChar.Position.DistanceTo(ProcessingZoneIAAEntryPos) < 2f)
        {
            Game.FadeScreenOut(100);
            Script.Wait(1500);
            Game.Player.Character.Position = ProcessingZoneIAASpawnPosIn;
            Game.Player.Character.Heading = 87.90f;
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
            Script.Wait(1500);
            Game.FadeScreenIn(400);
        }
        if (plrChar != null & plrChar.IsAlive && plrChar.Position.DistanceTo(ProcessingZoneIAAExitPos) < 2f)
        {
            Function.Call(Hash.DRAW_MARKER, 1, 2155.11f, 2920.93f, -62.90f, 0f, 0f, 0f, 0f, 0f, 0f, 0.6f, 0.6f, 0.5f, 240, 200, 80, 150, false, false, 2, false, false, false);
            DisplayHelpText("Press ~INPUT_CONTEXT~ to use the elevator and exit the IAA Data Processing Zone.");
        }
        if (Game.IsControlJustPressed(2, GTA.Control.Context) && plrChar.Position.DistanceTo(ProcessingZoneIAAExitPos) < 2f)
        {
            Game.Player.Character.FreezePosition = true;
            Game.FadeScreenOut(100);
            Script.Wait(1500);
            Game.Player.Character.Position = ProcessingZoneIAASpawnPosOut;
            Game.Player.Character.Heading = 247.04f;
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
            Game.Player.Character.FreezePosition = false;
            Script.Wait(1500);
            Game.FadeScreenIn(400);
        }

    }

    void IAAmbient()
    {
        List<int> Props = new List<int>();
        int LodDistance = 4000;            

        Func<int, Vector3, Vector3, bool, int> createProp = new Func<int, Vector3, Vector3, bool, int>(delegate(int hash, Vector3 pos, Vector3 rot, bool dynamic)
        {
            Model model = new Model(hash);
            model.Request(10000);
            Prop prop = GTA.World.CreateProp(model, pos, rot, dynamic, false);
            prop.Position = pos;
            prop.LodDistance = LodDistance;
            if (!dynamic)
                prop.FreezePosition = true;
            return prop.Handle;
        });

        bool Initialized = false;


        base.Tick += delegate (object sender, EventArgs args)
        {
            if (!Initialized)
            {

                //Peds in the IAA Processing Area
                World.CreatePed(PedHash.Casey, new Vector3(2132.98f, 2925.011f, -61.9019f), 240.648f); // Gruppe Sechs
                World.CreatePed(PedHash.CiaSec01SMM, new Vector3(2126.84f, 2923.135f, -61.9019f), 229.804f); // cia Sec 01
                World.CreatePed(PedHash.FemaleAgent, new Vector3(2064.385f, 2992.443f, -61.9019f), 258.863f); // Female Agent      

                World.CreatePed(PedHash.CiaSec01SMM, new Vector3(2059.363f, 2985.283f, -67.30146f), 158.00f); // cia Sec 01 (War Room)
                World.CreatePed(PedHash.CiaSec01SMM, new Vector3(2062.451f, 2981.445f, -67.30146f), 133.40f); // cia Sec 02 (War Room)
                World.CreatePed(PedHash.Highsec01SMM, new Vector3(2036.73f, 2943.746f, -61.90175f), 240.814f); // High Security 01     

                World.CreatePed(PedHash.Paper, new Vector3(2068.13f, 2993.655f, -67.70156f), 161.097f); // cia Head  
                World.CreatePed(PedHash.Bride, new Vector3(2068.888f, 2992.479f, -67.70156f), 129.982f); // cia Wife
                World.CreatePed(PedHash.Marine02SMM, new Vector3(2064.292f, 2990.72f, -67.70156f), 290.9466f); // Army Head

                World.CreatePed(PedHash.Marine01SMM, new Vector3(2064.385f, 2992.443f, -67.70156f), 323.9484f); // Army Security 01  
                World.CreatePed(PedHash.EdToh, new Vector3(2068.086f, 2989.504f, -67.70156f), 55.5085f); // Chinese Architect
                World.CreatePed(PedHash.Andreas, new Vector3(2066.459f, 2989.046f, -67.70156f), 2.787f); // Andreas Sanchez

                World.CreatePed(PedHash.Chimp, new Vector3(2023.166f, 3023.465f, -72.69173f), 194.353f); //  Labor Subject 01
                World.CreatePed(PedHash.Rhesus, new Vector3(2021.641f, 3018.443f, -72.69173f), 358.109F); // Labor Subject 02

                Initialized = true;
            }
                /* WORLD */
            Prop returnedProp;

        };
    }

}