namespace Business.Managers.Abstracts;

public interface IConversionManager
{
    string Get();

    void Clear();

    void Add(string digit, string digitValue);
}