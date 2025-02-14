// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WinRT;
using WinRT.Interop;

namespace ABI.System
{
    [Guid("9DE1C535-6AE1-11E0-84E1-18A905BCC53F"), EditorBrowsable(EditorBrowsableState.Never)]
#if EMBED
    internal
#else
    public
#endif
    static class EventHandler<T>
    {
        public static Guid PIID = GuidGenerator.CreateIID(typeof(global::System.EventHandler<T>));
        private static readonly global::System.Type Abi_Invoke_Type = Expression.GetDelegateType(new global::System.Type[] { typeof(void*), typeof(IntPtr), Marshaler<T>.AbiType, typeof(int) });

        private static readonly global::WinRT.Interop.IDelegateVftbl AbiToProjectionVftable;
        public static readonly IntPtr AbiToProjectionVftablePtr;

        static EventHandler()
        {
            AbiInvokeDelegate = global::System.Delegate.CreateDelegate(Abi_Invoke_Type, typeof(EventHandler<T>).GetMethod(nameof(Do_Abi_Invoke), BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new global::System.Type[] { Marshaler<T>.AbiType })
            );
            AbiToProjectionVftable = new global::WinRT.Interop.IDelegateVftbl
            {
                IUnknownVftbl = global::WinRT.Interop.IUnknownVftbl.AbiToProjectionVftbl,
                Invoke = Marshal.GetFunctionPointerForDelegate(AbiInvokeDelegate)
            };
            var nativeVftbl = ComWrappersSupport.AllocateVtableMemory(typeof(EventHandler<T>), Marshal.SizeOf<global::WinRT.Interop.IDelegateVftbl>());
            Marshal.StructureToPtr(AbiToProjectionVftable, nativeVftbl, false);
            AbiToProjectionVftablePtr = nativeVftbl;
        }

        public static global::System.Delegate AbiInvokeDelegate { get; }

        public static unsafe IObjectReference CreateMarshaler(global::System.EventHandler<T> managedDelegate) =>
            managedDelegate is null ? null : MarshalDelegate.CreateMarshaler(managedDelegate, PIID);

        public static unsafe ObjectReferenceValue CreateMarshaler2(global::System.EventHandler<T> managedDelegate) => 
            MarshalDelegate.CreateMarshaler2(managedDelegate, PIID);

        public static IntPtr GetAbi(IObjectReference value) =>
            value is null ? IntPtr.Zero : MarshalInterfaceHelper<global::System.EventHandler<T>>.GetAbi(value);

        public static unsafe global::System.EventHandler<T> FromAbi(IntPtr nativeDelegate)
        {
            return MarshalDelegate.FromAbi<global::System.EventHandler<T>>(nativeDelegate);
        }

        public static global::System.EventHandler<T> CreateRcw(IntPtr ptr)
        {
            return new global::System.EventHandler<T>(new NativeDelegateWrapper(ComWrappersSupport.GetObjectReferenceForInterface<IDelegateVftbl>(ptr, PIID)).Invoke);
        }

        [global::WinRT.ObjectReferenceWrapper(nameof(_nativeDelegate))]
#if !NET
        private sealed class NativeDelegateWrapper
#else
        private sealed class NativeDelegateWrapper : IWinRTObject
#endif
        {
            private readonly ObjectReference<global::WinRT.Interop.IDelegateVftbl> _nativeDelegate;

            public NativeDelegateWrapper(ObjectReference<global::WinRT.Interop.IDelegateVftbl> nativeDelegate)
            {
                _nativeDelegate = nativeDelegate;
            }

#if NET
            IObjectReference IWinRTObject.NativeObject => _nativeDelegate;
            bool IWinRTObject.HasUnwrappableNativeObject => true;
            private volatile ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> _queryInterfaceCache;
            private ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> MakeQueryInterfaceCache()
            {
                global::System.Threading.Interlocked.CompareExchange(ref _queryInterfaceCache, new ConcurrentDictionary<RuntimeTypeHandle, IObjectReference>(), null);
                return _queryInterfaceCache;
            }
            ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> IWinRTObject.QueryInterfaceCache => _queryInterfaceCache ?? MakeQueryInterfaceCache();

            private volatile ConcurrentDictionary<RuntimeTypeHandle, object> _additionalTypeData;
            private ConcurrentDictionary<RuntimeTypeHandle, object> MakeAdditionalTypeData()
            {
                global::System.Threading.Interlocked.CompareExchange(ref _additionalTypeData, new ConcurrentDictionary<RuntimeTypeHandle, object>(), null);
                return _additionalTypeData;
            }
            ConcurrentDictionary<RuntimeTypeHandle, object> IWinRTObject.AdditionalTypeData => _additionalTypeData ?? MakeAdditionalTypeData();
#endif

            public void Invoke(object sender, T args)
            {
                IntPtr ThisPtr = _nativeDelegate.ThisPtr;
                var abiInvoke = Marshal.GetDelegateForFunctionPointer(_nativeDelegate.Vftbl.Invoke, Abi_Invoke_Type);
                ObjectReferenceValue __sender = default;
                object __args = default;
                var __params = new object[] { ThisPtr, null, null };
                try
                {
                    __sender = MarshalInspectable<object>.CreateMarshaler2(sender);
                    __params[1] = MarshalInspectable<object>.GetAbi(__sender);
                    __args = Marshaler<T>.CreateMarshaler2(args);
                    __params[2] = Marshaler<T>.GetAbi(__args);
                    abiInvoke.DynamicInvokeAbi(__params);
                }
                finally
                {
                    MarshalInspectable<object>.DisposeMarshaler(__sender);
                    Marshaler<T>.DisposeMarshaler(__args);
                }
            }
        }

        public static IntPtr FromManaged(global::System.EventHandler<T> managedDelegate) => 
            CreateMarshaler2(managedDelegate).Detach();

        public static void DisposeMarshaler(IObjectReference value) => MarshalInterfaceHelper<global::System.EventHandler<T>>.DisposeMarshaler(value);

        public static void DisposeAbi(IntPtr abi) => MarshalInterfaceHelper<global::System.EventHandler<T>>.DisposeAbi(abi);

        private static unsafe int Do_Abi_Invoke<TAbi>(void* thisPtr, IntPtr sender, TAbi args)
        {
            try
            {
#if NET
                var invoke = ComWrappersSupport.FindObject<global::System.EventHandler<T>>(new IntPtr(thisPtr));
                invoke.Invoke(MarshalInspectable<object>.FromAbi(sender), Marshaler<T>.FromAbi(args));
#else
                global::WinRT.ComWrappersSupport.MarshalDelegateInvoke(new IntPtr(thisPtr), (global::System.EventHandler<T> invoke) =>
                {
                    invoke.Invoke(MarshalInspectable<object>.FromAbi(sender), Marshaler<T>.FromAbi(args));
                });
#endif
            }
            catch (global::System.Exception __exception__)
            {
                global::WinRT.ExceptionHelpers.SetErrorInfo(__exception__);
                return global::WinRT.ExceptionHelpers.GetHRForException(__exception__);
            }
            return 0;
        }
    }

    [Guid("c50898f6-c536-5f47-8583-8b2c2438a13b")]
    internal static class EventHandler
    {
#if !NET
        private delegate int Abi_Invoke(IntPtr thisPtr, IntPtr sender, IntPtr args);
#endif

        private static readonly global::WinRT.Interop.IDelegateVftbl AbiToProjectionVftable;
        public static readonly IntPtr AbiToProjectionVftablePtr;

        static unsafe EventHandler()
        {
            AbiToProjectionVftable = new global::WinRT.Interop.IDelegateVftbl
            {
                IUnknownVftbl = global::WinRT.Interop.IUnknownVftbl.AbiToProjectionVftbl,
#if !NET
                Invoke = Marshal.GetFunctionPointerForDelegate(AbiInvokeDelegate = (Abi_Invoke)Do_Abi_Invoke)
#else
                Invoke = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr, int>)&Do_Abi_Invoke
#endif
            };
            var nativeVftbl = ComWrappersSupport.AllocateVtableMemory(typeof(EventHandler), Marshal.SizeOf<global::WinRT.Interop.IDelegateVftbl>());
            Marshal.StructureToPtr(AbiToProjectionVftable, nativeVftbl, false);
            AbiToProjectionVftablePtr = nativeVftbl;
        }

#if !NET
        public static global::System.Delegate AbiInvokeDelegate { get; }
#endif

        private static readonly Guid IID = new(0xc50898f6, 0xc536, 0x5f47, 0x85, 0x83, 0x8b, 0x2c, 0x24, 0x38, 0xa1, 0x3b);

        public static unsafe IObjectReference CreateMarshaler(global::System.EventHandler managedDelegate) =>
            managedDelegate is null ? null : MarshalDelegate.CreateMarshaler(managedDelegate, IID);

        public static unsafe ObjectReferenceValue CreateMarshaler2(global::System.EventHandler managedDelegate) =>
            MarshalDelegate.CreateMarshaler2(managedDelegate, IID);

        public static IntPtr GetAbi(IObjectReference value) =>
            value is null ? IntPtr.Zero : MarshalInterfaceHelper<global::System.EventHandler<object>>.GetAbi(value);

        public static unsafe global::System.EventHandler FromAbi(IntPtr nativeDelegate)
        {
            return MarshalDelegate.FromAbi<global::System.EventHandler>(nativeDelegate);
        }

        public static global::System.EventHandler CreateRcw(IntPtr ptr)
        {
            return new global::System.EventHandler(new NativeDelegateWrapper(ComWrappersSupport.GetObjectReferenceForInterface<IDelegateVftbl>(ptr, IID)).Invoke);
        }

        [global::WinRT.ObjectReferenceWrapper(nameof(_nativeDelegate))]
#if !NET
        private sealed class NativeDelegateWrapper
#else
        private sealed class NativeDelegateWrapper : IWinRTObject
#endif
        {
            private readonly ObjectReference<global::WinRT.Interop.IDelegateVftbl> _nativeDelegate;

            public NativeDelegateWrapper(ObjectReference<global::WinRT.Interop.IDelegateVftbl> nativeDelegate)
            {
                _nativeDelegate = nativeDelegate;
            }

#if NET
            IObjectReference IWinRTObject.NativeObject => _nativeDelegate;
            bool IWinRTObject.HasUnwrappableNativeObject => true;
            private volatile ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> _queryInterfaceCache;
            private ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> MakeQueryInterfaceCache()
            {
                global::System.Threading.Interlocked.CompareExchange(ref _queryInterfaceCache, new ConcurrentDictionary<RuntimeTypeHandle, IObjectReference>(), null);
                return _queryInterfaceCache;
            }
            ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> IWinRTObject.QueryInterfaceCache => _queryInterfaceCache ?? MakeQueryInterfaceCache();

            private volatile ConcurrentDictionary<RuntimeTypeHandle, object> _additionalTypeData;
            private ConcurrentDictionary<RuntimeTypeHandle, object> MakeAdditionalTypeData()
            {
                global::System.Threading.Interlocked.CompareExchange(ref _additionalTypeData, new ConcurrentDictionary<RuntimeTypeHandle, object>(), null);
                return _additionalTypeData;
            }
            ConcurrentDictionary<RuntimeTypeHandle, object> IWinRTObject.AdditionalTypeData => _additionalTypeData ?? MakeAdditionalTypeData();
#endif

            public unsafe void Invoke(object sender, EventArgs args)
            {
                IntPtr ThisPtr = _nativeDelegate.ThisPtr;
#if !NET
                var abiInvoke = Marshal.GetDelegateForFunctionPointer<Abi_Invoke>(_nativeDelegate.Vftbl.Invoke);
#else
                var abiInvoke = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr, int>)(_nativeDelegate.Vftbl.Invoke);
#endif
                ObjectReferenceValue __sender = default;
                ObjectReferenceValue __args = default;
                try
                {
                    __sender = MarshalInspectable<object>.CreateMarshaler2(sender);
                    __args = MarshalInspectable<EventArgs>.CreateMarshaler2(args);
                    global::WinRT.ExceptionHelpers.ThrowExceptionForHR(abiInvoke(
                        ThisPtr,
                        MarshalInspectable<object>.GetAbi(__sender),
                        MarshalInspectable<EventArgs>.GetAbi(__args)));
                }
                finally
                {
                    MarshalInspectable<object>.DisposeMarshaler(__sender);
                    MarshalInspectable<EventArgs>.DisposeMarshaler(__args);
                }
            }
        }

        public static IntPtr FromManaged(global::System.EventHandler managedDelegate) => 
            CreateMarshaler2(managedDelegate).Detach();

        public static void DisposeMarshaler(IObjectReference value) => MarshalInterfaceHelper<global::System.EventHandler<object>>.DisposeMarshaler(value);

        public static void DisposeAbi(IntPtr abi) => MarshalInterfaceHelper<global::System.EventHandler<object>>.DisposeAbi(abi);

#if NET
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
#endif
        private static unsafe int Do_Abi_Invoke(IntPtr thisPtr, IntPtr sender, IntPtr args)
        {
            try
            {
#if NET
                var invoke = ComWrappersSupport.FindObject<global::System.EventHandler>(thisPtr);
                invoke.Invoke(
                    MarshalInspectable<object>.FromAbi(sender),
                    MarshalInspectable<object>.FromAbi(args) as EventArgs ?? EventArgs.Empty);
#else
                global::WinRT.ComWrappersSupport.MarshalDelegateInvoke(thisPtr, (global::System.EventHandler invoke) =>
                {
                    invoke.Invoke(
                        MarshalInspectable<object>.FromAbi(sender),
                        MarshalInspectable<object>.FromAbi(args) as EventArgs ?? EventArgs.Empty);
                });
#endif
            }
            catch (global::System.Exception __exception__)
            {
                global::WinRT.ExceptionHelpers.SetErrorInfo(__exception__);
                return global::WinRT.ExceptionHelpers.GetHRForException(__exception__);
            }
            return 0;
        }
    }

    internal sealed unsafe class EventHandlerEventSource : EventSource<global::System.EventHandler>
    {
        internal EventHandlerEventSource(IObjectReference obj,
            delegate* unmanaged[Stdcall]<global::System.IntPtr, global::System.IntPtr, out global::WinRT.EventRegistrationToken, int> addHandler,
            delegate* unmanaged[Stdcall]<global::System.IntPtr, global::WinRT.EventRegistrationToken, int> removeHandler)
            : base(obj, addHandler, removeHandler)
        {
        }

        protected override ObjectReferenceValue CreateMarshaler(global::System.EventHandler del) => 
            EventHandler.CreateMarshaler2(del);

        protected override State CreateEventState() =>
            new EventState(_obj.ThisPtr, _index);

        private sealed class EventState : State
        {
            public EventState(IntPtr obj, int index)
                : base(obj, index)
            {
            }

            protected override Delegate GetEventInvoke()
            {
                global::System.EventHandler handler = (global::System.Object obj, global::System.EventArgs e) =>
                {
                    var localDel = (global::System.EventHandler) del;
                    if (localDel != null)
                        localDel.Invoke(obj, e);
                };
                return handler;
            }
        }
    }
}
