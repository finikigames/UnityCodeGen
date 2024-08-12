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
    }
}
