namespace UnityCodeGen
{
    public static class UnityCodeGenUtility
    {
        public const string defaultFolderPath = "Assets/UnityCodeGen.Generated";

        public static void Generate()
        {
            ScriptFileGenerator.Generate();
        }
        
        public static void GenerateByType<T>() where T : class, ICodeGenerator {
            ScriptFileGenerator.GenerateByType<T>();
        }
        
        public static void GenerateClassByType<T>(IClassConfig config) where T : class, ICodeGenerator{
            ScriptFileGenerator.GenerateByType<T>(config);
        }
    }
}
