using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace pokeemerald_pokefirered_mergetool
{
    public class Connection
    {
        public string map { get; set; }
        public int offset { get; set; }
        public string direction { get; set; }
    }

    public class ObjectEvent
    {
        public string graphics_id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int elevation { get; set; }
        public string movement_type { get; set; }
        public int movement_range_x { get; set; }
        public int movement_range_y { get; set; }
        public string trainer_type { get; set; }
        public string trainer_sight_or_berry_tree_id { get; set; }
        public string script { get; set; }
        public string flag { get; set; }
    }

    public class WarpEvent
    {
        public int x { get; set; }
        public int y { get; set; }
        public int elevation { get; set; }
        public string dest_map { get; set; }
        public int dest_warp_id { get; set; }
    }

    public class CoordEvent
    {
        public string type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int elevation { get; set; }
        public string var { get; set; }
        public string var_value { get; set; }
        public string script { get; set; }
    }

    public class BgEvent
    {
        public string type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int elevation { get; set; }
        public string player_facing_dir { get; set; }
        public string script { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public string name { get; set; }
        public string layout { get; set; }
        public string music { get; set; }
        public string region_map_section { get; set; }
        public bool requires_flash { get; set; }
        public string weather { get; set; }
        public string map_type { get; set; }
        public bool allow_cycling { get; set; }
        public bool allow_escaping { get; set; }
        public bool allow_running { get; set; }
        public bool show_map_name { get; set; }
        public int floor_number { get; set; }
        public string battle_scene { get; set; }
        public IList<Connection> connections { get; set; }
        public IList<ObjectEvent> object_events { get; set; }
        public IList<WarpEvent> warp_events { get; set; }
        public IList<CoordEvent> coord_events { get; set; }
        public IList<BgEvent> bg_events { get; set; }

        public void UpdateJsonWithPrefixes()
        {
            this.id = this.id.Insert(4, MergeTool.PREFIX);
            this.name = MergeTool.PREFIX + this.name;
            this.layout = this.layout.Insert(7, MergeTool.PREFIX);
            this.region_map_section = this.region_map_section.Insert(7, MergeTool.PREFIX);

            if (connections != null)
                for (int i = 0; i < this.connections.Count; i++)
                {
                    this.connections[i].map = this.connections[i].map.Insert(4, MergeTool.PREFIX);
                }

            if (object_events != null)
                for (int i = 0; i < this.object_events.Count; i++)
                {
                    this.object_events[i].script = MergeTool.PREFIX + this.object_events[i].script;
                }

            if (warp_events != null)
                for (int i = 0; i < this.warp_events.Count; i++)
                {
                    this.warp_events[i].dest_map = this.warp_events[i].dest_map.Insert(4, MergeTool.PREFIX);
                }

            if (coord_events != null)
                for (int i = 0; i < this.coord_events.Count; i++)
                {
                    this.coord_events[i].script = MergeTool.PREFIX + this.coord_events[i].script;
                }

            if (bg_events != null)
                for (int i = 0; i < this.bg_events.Count; i++)
                {
                    this.bg_events[i].script = MergeTool.PREFIX + this.bg_events[i].script;
                }
        }
    }
}


