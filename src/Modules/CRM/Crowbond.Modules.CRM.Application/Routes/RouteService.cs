using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.Routes;
public class RouteService
{
    public bool IsDayActive(string routeMask, Weekday weekday)
    {
        if (string.IsNullOrEmpty(routeMask) || routeMask.Length != 7)
        {
            throw new ArgumentException("Invalid route day mask");
        }

        return routeMask[(int)weekday - 1] == '1';
    }

    public string ActivateDay(string routeMask, Weekday weekday)
    {
        if (string.IsNullOrEmpty(routeMask) || routeMask.Length != 7)
        {
            throw new ArgumentException("Invalid route day mask");
        }

        char[] chars = routeMask.ToCharArray();
        chars[(int)weekday] = '1';
        return new string(chars);
    }

    public string DeactivateDay(string routeMask, Weekday weekday)
    {
        if (string.IsNullOrEmpty(routeMask) || routeMask.Length != 7)
        {
            throw new ArgumentException("Invalid route day mask");
        }

        char[] chars = routeMask.ToCharArray();
        chars[(int)weekday] = '0';
        return new string(chars);
    }
}
