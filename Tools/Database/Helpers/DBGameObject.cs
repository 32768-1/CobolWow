//using System.Linq;
//using System.Collections.Generic;

//using CobolWow.Game.Entitys;
//using CobolWow.Tools.Database.Tables;

//namespace CobolWow.Tools.Database.Helpers
//{
//   public class DBGameObject
//    {
//        private static List<GameObject> GameObjectCache;
//        public static List<GameObject> GameObjects
//        {
//            get
//            {
//                if (GameObjectCache == null) GameObjectCache = DB.World.Table<GameObject>().ToList();

//                return GameObjectCache;
//            }
//        }

//        private static List<GameObjectTemplate> GameObjectTemplateCache;
//        public static List<GameObjectTemplate> GameObjectTemplates
//        {
//            get
//            {
//                if (GameObjectTemplateCache == null) GameObjectTemplateCache = DB.World.Table<GameObjectTemplate>().ToList();

//                return GameObjectTemplateCache;
//            }
//        }

//        public static List<GameObject> GetGameObjects(PlayerEntity entity, float Radius)
//        {
//            return GameObjects.FindAll((go) => go.Map == entity.Character.map && Helper.Distance(entity.Character.position_x, entity.Character.position_y, go.X, go.Y) < Radius);
//        }

//        public static GameObjectTemplate GetGameObjectTemplate(uint Entry)
//        {
//            return GameObjectTemplates.ToList<GameObjectTemplate>().Find((got) => got.Entry == Entry);
//        }
//    }
//}
