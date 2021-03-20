using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Moon_Launcher.InjectorShit
{
	// Token: 0x02000009 RID: 9
	public class ProcessHelper
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000024B8 File Offset: 0x000006B8
		public static Process StartProcess(string path, bool shouldFreeze, string extraArgs = "")
		{
			Process process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = path,
					Arguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=f7b9gah4h5380d10f721dd6a " + extraArgs
				}
			};
			process.Start();
			if (shouldFreeze)
			{
				foreach (object obj in process.Threads)
				{
					ProcessThread processThread = (ProcessThread)obj;
					ProcessHelper.SuspendThread(ProcessHelper.OpenThread(2, false, processThread.Id));
				}
			}
			return process;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002554 File Offset: 0x00000754
		public static void InjectDll(int processId, string path)
		{
			IntPtr hProcess = ProcessHelper.OpenProcess(1082, false, processId);
			IntPtr procAddress = ProcessHelper.GetProcAddress(ProcessHelper.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
			uint num = (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char)));
			IntPtr intPtr = ProcessHelper.VirtualAllocEx(hProcess, IntPtr.Zero, num, 12288U, 4U);
			UIntPtr uintPtr;
			ProcessHelper.WriteProcessMemory(hProcess, intPtr, Encoding.Default.GetBytes(path), num, out uintPtr);
			ProcessHelper.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAddress, intPtr, 0U, IntPtr.Zero);
		}

		// Token: 0x06000022 RID: 34
		[DllImport("kernel32.dll")]
		public static extern int SuspendThread(IntPtr hThread);

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll")]
		public static extern int ResumeThread(IntPtr hThread);

		// Token: 0x06000024 RID: 36
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x06000026 RID: 38
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x06000027 RID: 39
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000028 RID: 40
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06000029 RID: 41
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x0600002A RID: 42
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

		// Token: 0x0600002B RID: 43
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
	}
}
