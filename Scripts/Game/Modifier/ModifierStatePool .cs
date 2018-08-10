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

        T result = (T)modifierStatesDic[typeof(T).ToString()][0];

        modifierStatesDic[typeof(T).ToString()].RemoveAt(0);


        return result;
    }

    public IModifierState GetModifierState(string modifierStateName)
    {
        IModifierState result= modifierStatesDic[modifierStateName][0];

        modifierStatesDic[modifierStateName].RemoveAt(0);

        return result;
    }


    private bool Contains(string modifierStateName)
    {
        return modifierStatesDic.ContainsKey(modifierStateName);
    }
}
