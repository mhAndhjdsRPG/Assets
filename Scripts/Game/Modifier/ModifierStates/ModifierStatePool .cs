using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ModifierStatePool
{
    private Dictionary<string, List<IModifierState>> modifierStatesDic=new Dictionary<string, List<IModifierState>>();

    public bool Have(string modifierStateName)
    {
        return Contains(modifierStateName) && modifierStatesDic[modifierStateName].Count > 0;
    }

    public void StoreModifierState(IModifierState state)
    {
        if (!Contains(state.Name))
        {
            modifierStatesDic.Add(state.Name, new List<IModifierState>());
        }

        modifierStatesDic[state.Name].Add(state);
    }

    

    public T GetModifierState<T>()where T:IModifierState
    {
        return (T)modifierStatesDic[typeof(T).ToString()][0];
    }

    public IModifierState GetModifierState(string modifierStateName)
    {
        return modifierStatesDic[modifierStateName][0];
    }


    private bool Contains(string modifierStateName)
    {
        return modifierStatesDic.ContainsKey(modifierStateName);
    }
}
