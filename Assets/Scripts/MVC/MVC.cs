using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC 
{
    public static MVC instance = new MVC();
    private MVC() { }

    Dictionary<string, ViewBase> views = new Dictionary<string, ViewBase>();
    Dictionary<string, ModelBase> models = new Dictionary<string, ModelBase>();
    Dictionary<string, Type> ctrls = new Dictionary<string, Type>();

    /// <summary>
    /// 注册视图
    /// </summary>
    /// <param name="view"></param>
    public void RegisterView(ViewBase view)
    {
        
        if (views.ContainsKey(view.Name))
            return;
        views[view.Name] = view;
    }

    /// <summary>
    ///  注册模型
    /// </summary>
    /// <param name="model"></param>
    public void RegisterModel(ModelBase model)
    {
        if (models.ContainsKey(model.Name))
            return;
        models[model.Name] = model;
    }

    /// <summary>
    /// 注册控制器
    /// </summary>
    /// <param name="ctrl"></param>
    public void RegisterCtrl(Type ctrl)
    {
        if (ctrls.ContainsKey(ctrl.Name))
            return;
        ctrls[ctrl.Name] = ctrl;
    }


    /// <summary>
    /// 发布
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="eventParams"></param>
    public void SendEvents(string eventName,object eventParams)
    {
        if (ctrls.ContainsKey(eventName)){
            Type tcb = ctrls[eventName];
            ControllerBase cb = Activator.CreateInstance(tcb) as ControllerBase;
            cb.Excute(eventParams);
        }

        foreach(var view in views)
        {
            if (view.Value.interestEvents.Contains(eventName))
            {
                view.Value.HandlerEvents(eventName, eventParams);
            }
        }
    }
}
