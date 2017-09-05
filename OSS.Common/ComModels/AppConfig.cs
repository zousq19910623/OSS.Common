﻿#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSSCommon - 通用应用账号配置信息
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2017-5-25
*       
*****************************************************************************/

#endregion

using System;
using System.Threading;
using OSS.Common.Plugs;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 通用应用账号配置信息  
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 应用账号AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用账号AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 操作管理类型
        /// </summary>
        public AppOperateMode OperateMode { get; set; } = AppOperateMode.BySelf;

        /// <summary>
        ///  代理应用的账号AppId
        /// </summary>
        public string AgentAppId { get; set; }
    }

    /// <summary>
    /// 应用操作类型
    /// </summary>
    public enum AppOperateMode
    {
        /// <summary>
        ///  自管理
        /// </summary>
        BySelf,
        /// <summary>
        /// 代理操作
        /// </summary>
        ByAgent
    }


    /// <summary>
    ///   通用配置基类
    /// </summary>
    /// <typeparam name="TConfigType"></typeparam>
    public class BaseConfigProvider<TConfigType>
        where TConfigType : class, new()
    {

        #region  接口配置信息

        private static readonly AsyncLocal<TConfigType> _contextConfig = new AsyncLocal<TConfigType>();
        private readonly TConfigType _config;

        /// <summary>
        ///  设置上下文配置信息，当前配置在当前上下文中有效
        /// </summary>
        /// <param name="config"></param>
        public virtual void SetContextConfig(TConfigType config)
        {
            _contextConfig.Value = config;
        }

        /// <summary>
        /// 微信接口配置
        /// </summary>
        public TConfigType ApiConfig
        {
            get
            {
                if (_config != null)
                {
                    return _config;
                }

                if (_contextConfig.Value != null)
                {
                    return _contextConfig.Value;
                }
                throw new ArgumentNullException("当前配置信息为空，请通过构造函数中赋值，或者SetContextConfig方法设置当前上下文配置信息");
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseConfigProvider(TConfigType config)
        {
            if (config != null)
                _config = config;
        }


        #endregion

        /// <summary>
        ///   当前模块名称
        /// </summary>
        public string ModuleName { get; set; } = ModuleNames.Default;
    }

}
