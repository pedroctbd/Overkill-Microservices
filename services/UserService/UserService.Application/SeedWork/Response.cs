namespace UserService.Application.SeedWork;

public class Response
{
    public object Data { get; private set; }

    public Dictionary<string, List<string>> Errors { get; private set; }

    public Response()
    {
        Errors = new Dictionary<string, List<string>>();
    }

    public Response(object data)
    {
        Data = data;
        Errors = new Dictionary<string, List<string>>();
    }

    public void AddError(string name, string message)
    {
        if (!Errors.ContainsKey(name))
            Errors.Add(name, new List<string>());

        Errors[name].Add(message);
    }
}
