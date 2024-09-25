using System.Collections.Generic;
namespace UnityCodeGen
{
    public sealed class GeneratorContext
    {
        private List<CodeText> _codeList = new List<CodeText>();
        internal IReadOnlyList<CodeText> codeList => _codeList;

        private string _overrideFolderPath = null;
        internal string overrideFolderPath => _overrideFolderPath;
        
        private string _contextName = "DefaultClassName";
        private string _classNameFormat = "{0}.cs";
        public string ContextName => ClassConfig.ClassName;
        
        public IClassConfig ClassConfig { get; set; }
        
        public void AddCode(string fileName, string text)
        {
            _codeList.Add(new CodeText() { fileName = fileName, text = text });
        }
        
        public void AddCodeWithContextName(string text)
        {
            var className = string.Format(_classNameFormat, ContextName);
            _codeList.Add(new CodeText() { fileName = className, text = text });
        }

        public void OverrideFolderPath(string path)
        {
            _overrideFolderPath = path;
        }

        public void SetContextName(string className) {
            _contextName = className;
        }

        public void SetClassNameFormat(string classNameFormat) {
            _classNameFormat = classNameFormat;
        }
    }
}