using System.Data;

namespace DataManager.Generic.Execution
{
    public class Command
    {
        public Dictionary<string, Parameter> Parameters;

        public string Name { get; }
        public string Description { get; }

        public Command(string name, string description)
        {
            Name = name;
            Description = description;
            Parameters = new Dictionary<string, Parameter>();
        }

        public Command()
        {
            Parameters = new Dictionary<string, Parameter>();
        }

        public void AddParameter(string name, object value, string type, string direction, int size = -1)
        {
            Parameter commandParameter;

            if (string.IsNullOrEmpty(name))
                throw new Exceptions.ExecutionException("Parameter name can't be empty.");

            if (Parameters.ContainsKey(name))
                throw new Exceptions.ExecutionException("Parameter already exists");

            commandParameter = new Parameter(name, value, type, direction, size);

            Parameters.Add(name, commandParameter);
        }
    }
}
