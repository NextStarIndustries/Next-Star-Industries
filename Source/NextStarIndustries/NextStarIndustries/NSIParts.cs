using RUI.Icons.Selectable;
using System.Collections.Generic;
using static KSP.UI.Screens.PartCategorizer;
using UnityEngine;

namespace NextStarIndustries
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class NISCategoryModule : MonoBehaviour
    {
        readonly List<AvailablePart> availableParts = new List<AvailablePart>();
        readonly List<AvailablePart> nwParts = new List<AvailablePart>();
        readonly List<AvailablePart> cwParts = new List<AvailablePart>();
        readonly List<AvailablePart> rdParts = new List<AvailablePart>();
        readonly List<AvailablePart> probeParts = new List<AvailablePart>();
        readonly List<AvailablePart> engineParts = new List<AvailablePart>();
        void Awake()
        {
            availableParts.Clear();
            var countap = PartLoader.LoadedPartsList.Count;
            for (int a = 0; a < countap; ++a)
            {
                var avPart = PartLoader.LoadedPartsList[a];
                if (!avPart.partPrefab) continue;
                if (avPart.author.Contains("NSI"))
                {
                    availableParts.Add(avPart);
                }
            }
            nwParts.Clear();
            var countnp = PartLoader.LoadedPartsList.Count;
            for (int n = 0; n < countnp; ++n)
            {
                var nPart = PartLoader.LoadedPartsList[n];
                if (!nPart.partPrefab) continue;
                if (nPart.author.Contains("NSINuclear"))
                {
                    nwParts.Add(nPart);
                }

            }
            cwParts.Clear();
            var countcp = PartLoader.LoadedPartsList.Count;
            for (int c = 0; c < countcp; ++c)
            {
                var cPart = PartLoader.LoadedPartsList[c];
                if (!cPart.partPrefab) continue;
                if (cPart.author.Contains("NSIConventional"))
                {
                    cwParts.Add(cPart);
                }
            }
            rdParts.Clear();
            var countrdp = PartLoader.LoadedPartsList.Count;
            for (int r = 0; r < countcp; ++r)
            {
                var rdPart = PartLoader.LoadedPartsList[r];
                if (!rdPart.partPrefab) continue;
                if (rdPart.author.Contains("NSIRD"))
                {
                    rdParts.Add(rdPart);
                }
            }
            probeParts.Clear();
            var countpp = PartLoader.LoadedPartsList.Count;
            for (int p = 0; p < countcp; ++p)
            {
                var prPart = PartLoader.LoadedPartsList[p];
                if (!prPart.partPrefab) continue;
                if (prPart.author.Contains("NSIProbes"))
                {
                    probeParts.Add(prPart);
                }
            }
            engineParts.Clear();
            var countep = PartLoader.LoadedPartsList.Count;
            for (int e = 0; e < countcp; ++e)
            {
                var ePart = PartLoader.LoadedPartsList[e];
                if (!ePart.partPrefab) continue;
                if (ePart.author.Contains("NSIEngines"))
                {
                    engineParts.Add(ePart);
                }
            }

            GameEvents.onGUIEditorToolbarReady.Add(NSICategory);
            Debug.Log("NSI Category is Awake");
            Debug.Log("NSI aPart Count: " + availableParts.Count);
            Debug.Log("NSI nPart Count: " + nwParts.Count);
            Debug.Log("NSI cPart Count: " + cwParts.Count);
            Debug.Log("NSI rdPart Count: " + rdParts.Count);
            Debug.Log("NSI pPart Count: " + probeParts.Count);
            Debug.Log("NSI ePart Count: " + engineParts.Count);
        }

        bool Ap(AvailablePart avPart)
        {
            return availableParts.Contains(avPart);
        }
        bool Np(AvailablePart nPart)
        {
            return nwParts.Contains(nPart);
        }
        bool Cp(AvailablePart cPart)
        {
            return cwParts.Contains(cPart);
        }
        bool Rdp(AvailablePart rdPart)
        {
            return rdParts.Contains(rdPart);
        }
        bool Pp(AvailablePart prPart)
        {
            return probeParts.Contains(prPart);
        }
        bool Ep(AvailablePart ePart)
        {
            return engineParts.Contains(ePart);
        }

        void NSICategory()
        {
            Debug.Log("NSI Category Added");
            //Icon Textures
            Texture2D NSIIconN = GameDatabase.Instance.GetTexture("NSI/Textures/NSIIcon", false);
            Texture2D NSIIconS = GameDatabase.Instance.GetTexture("NSI/Textures/NSIIconActive", false);
            Texture2D NSIIconNW = GameDatabase.Instance.GetTexture("NSI/Textures/NSIIconNW", false);
            Texture2D NSIIconCW = GameDatabase.Instance.GetTexture("NSI/Textures/NSIIconCW", false);
            Texture2D NSIIconRD = GameDatabase.Instance.GetTexture("NSI/Textures/NSIIconRD", false);
            Texture2D NSIIconProbes = GameDatabase.Instance.GetTexture("Squad/PartList/SimpleIcons/R&D_node_icon_largeprobes", false);
            Texture2D NSIIconEngines = GameDatabase.Instance.GetTexture("Squad/PartList/SimpleIcons/R&D_node_icon_advancedmotors", false);
            //Icon Main Category
            Icon NSI = new Icon("NSI", NSIIconN, NSIIconS, false);
            //Icons SubCategories
            Icon NSINuclear = new Icon("Nuclear Weapons", NSIIconNW, NSIIconNW, false);
            Icon NSIConventional = new Icon("Conventional Weapons", NSIIconCW, NSIIconCW, false);
            Icon NSIRD = new Icon("R&D Parts", NSIIconRD, NSIIconRD, false);
            Icon NSIProbe = new Icon("Probes", NSIIconProbes, NSIIconProbes, false);
            Icon NSIEngine = new Icon("Engines", NSIIconEngines, NSIIconEngines, false);
            //Main Category
            Category filter = AddCustomFilter("NSI", "Next Star Industries", NSI, Color.red);
            //Sub Categories
            AddCustomSubcategoryFilter(filter, "NSI", "Next Star Industries", NSI, p => Ap(p));
            AddCustomSubcategoryFilter(filter, "Nuclear", "Nuclear Weapons", NSINuclear, nwp => Np(nwp));
            AddCustomSubcategoryFilter(filter, "Conventional", "Conventional Weapons", NSIConventional, cwp => Cp(cwp));
            AddCustomSubcategoryFilter(filter, "R&D", "Research and Development", NSIRD, radp => Rdp(radp));
            AddCustomSubcategoryFilter(filter, "NSIProbes", "NSI Probe Cores", NSIProbe, prp => Pp(prp));
            AddCustomSubcategoryFilter(filter, "NSIEngines", "NSI Engines", NSIEngine, enp => Ep(enp));
        }
    }
}
