using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enumerators
    /// </summary>
    public static class Enums

    {
        /// <summary>
        /// Log
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// Level
            /// </summary>
            [Flags]
            public enum Level
            {
                /// <summary>
                /// None
                /// </summary>
                None = 0,
                /// <summary>
                /// Debug
                /// </summary>
                Debug = 1,
                /// <summary>
                /// Information
                /// </summary>
                Info = 2,
                /// <summary>
                /// Warning
                /// </summary>
                Warning = 4,
                /// <summary>
                /// Error
                /// </summary>
                Error = 8
            }
        }

        /// <summary>
        /// Memory
        /// </summary>
        public static class Memory
        {
            /// <summary>
            /// Area
            /// </summary>
            [Flags]
            public enum Area
            {
                /// <summary>
                /// 
                /// </summary>
                None = 0,
                /// <summary>
                /// Combined Page List
                /// </summary>
                CombinedPageList = 1,
                /// <summary>
                /// Modified Page List
                /// </summary>
                ModifiedPageList = 2,
                /// <summary>
                /// Processes Working Set
                /// </summary>
                ProcessesWorkingSet = 4,
                /// <summary>
                /// Standby List
                /// </summary>
                StandbyList = 8,
                /// <summary>
                /// Standby List (Low Priority)
                /// </summary>
                StandbyListLowPriority = 16,
                /// <summary>
                /// System Working Set
                /// </summary>
                SystemWorkingSet = 32
            }
        }

        internal static class Windows
        {
            /// <summary>
            /// Windows system error codes
            /// </summary>
            /// <remarks>
            /// https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes
            /// </remarks>
            internal enum SystemErrorCode
            {
                /// <summary>
                /// The operation completed successfully.
                /// </summary>
                ERROR_SUCCESS = 0,
                /// <summary>
                /// Incorrect function.
                /// </summary>
                ERROR_INVALID_FUNCTION = 1,
                /// <summary>
                /// The system cannot find the file specified.
                /// </summary>
                ERROR_FILE_NOT_FOUND = 2,
                /// <summary>
                /// The system cannot find the path specified.
                /// </summary>
                ERROR_PATH_NOT_FOUND = 3,
                /// <summary>
                /// The system cannot open the file.
                /// </summary>
                ERROR_TOO_MANY_OPEN_FILES = 4,
                /// <summary>
                /// Access is denied.
                /// </summary>
                ERROR_ACCESS_DENIED = 5,
                /// <summary>
                /// The handle is invalid.
                /// </summary>
                ERROR_INVALID_HANDLE = 6,
                /// <summary>
                /// The storage control blocks were destroyed.
                /// </summary>
                ERROR_ARENA_TRASHED = 7,
                /// <summary>
                /// Not enough storage is available to process this command.
                /// </summary>
                ERROR_NOT_ENOUGH_MEMORY = 8,
                /// <summary>
                /// The storage control block address is invalid.
                /// </summary>
                ERROR_INVALID_BLOCK = 9,
                /// <summary>
                /// The environment is incorrect.
                /// </summary>
                ERROR_BAD_ENVIRONMENT = 10,
                /// <summary>
                /// An attempt was made to load a program with an incorrect format.
                /// </summary>
                ERROR_BAD_FORMAT = 11,
                /// <summary>
                /// The access code is invalid.
                /// </summary>
                ERROR_INVALID_ACCESS = 12,
                /// <summary>
                /// The data is invalid.
                /// </summary>
                ERROR_INVALID_DATA = 13,
                /// <summary>
                /// Not enough storage is available to complete this operation.
                /// </summary>
                ERROR_OUTOFMEMORY = 14,
                /// <summary>
                /// The system cannot find the drive specified.
                /// </summary>
                ERROR_INVALID_DRIVE = 15,
                /// <summary>
                /// The directory cannot be removed.
                /// </summary>
                ERROR_CURRENT_DIRECTORY = 16,
                /// <summary>
                /// The system cannot move the file to a different disk drive.
                /// </summary>
                ERROR_NOT_SAME_DEVICE = 17,
                /// <summary>
                /// There are no more files.
                /// </summary>
                ERROR_NO_MORE_FILES = 18,
                /// <summary>
                /// The media is write protected.
                /// </summary>
                ERROR_WRITE_PROTECT = 19,
                /// <summary>
                /// The system cannot find the device specified.
                /// </summary>
                ERROR_BAD_UNIT = 20,
                /// <summary>
                /// The device is not ready.
                /// </summary>
                ERROR_NOT_READY = 21,
                /// <summary>
                /// The device does not recognize the command.
                /// </summary>
                ERROR_BAD_COMMAND = 22,
                /// <summary>
                /// Data error (cyclic redundancy check).
                /// </summary>
                ERROR_CRC = 23,
                /// <summary>
                /// The program issued a command but the command length is incorrect.
                /// </summary>
                ERROR_BAD_LENGTH = 24,
                /// <summary>
                /// The drive cannot locate a specific area or track on the disk.
                /// </summary>
                ERROR_SEEK = 25,
                /// <summary>
                /// The specified disk or diskette cannot be accessed.
                /// </summary>
                ERROR_NOT_DOS_DISK = 26,
                /// <summary>
                /// The drive cannot find the sector requested.
                /// </summary>
                ERROR_SECTOR_NOT_FOUND = 27,
                /// <summary>
                /// The printer is out of paper.
                /// </summary>
                ERROR_OUT_OF_PAPER = 28,
                /// <summary>
                /// The system cannot write to the specified device.
                /// </summary>
                ERROR_WRITE_FAULT = 29,
                /// <summary>
                /// The system cannot read from the specified device.
                /// </summary>
                ERROR_READ_FAULT = 30,
                /// <summary>
                /// A device attached to the system is not functioning.
                /// </summary>
                ERROR_GEN_FAILURE = 31,
                /// <summary>
                /// The process cannot access the file because it is being used by another process.
                /// </summary>
                ERROR_SHARING_VIOLATION = 32,
                /// <summary>
                /// The process cannot access the file because another process has locked a portion of the file.
                /// </summary>
                ERROR_LOCK_VIOLATION = 33,
                /// <summary>
                /// The wrong diskette is in the drive. Insert %2 (Volume Serial Number: %3) into drive %1.
                /// </summary>
                ERROR_WRONG_DISK = 34,
                /// <summary>
                /// Too many files opened for sharing.
                /// </summary>
                ERROR_SHARING_BUFFER_EXCEEDED = 36,
                /// <summary>
                /// Reached the end of the file.
                /// </summary>
                ERROR_HANDLE_EOF = 38,
                /// <summary>
                /// The disk is full.
                /// </summary>
                ERROR_HANDLE_DISK_FULL = 39,
                /// <summary>
                /// The request is not supported.
                /// </summary>
                ERROR_NOT_SUPPORTED = 50,
                /// <summary>
                /// Windows cannot find the network path. Verify that the network path is correct and the destination computer is not busy or turned off. If Windows still cannot find the network path, contact your network administrator.
                /// </summary>
                ERROR_REM_NOT_LIST = 51,
                /// <summary>
                /// You were not connected because a duplicate name exists on the network. If joining a domain, go to System in Control Panel to change the computer name and try again. If joining a workgroup, choose another workgroup name.
                /// </summary>
                ERROR_DUP_NAME = 52,
                /// <summary>
                /// The network path was not found.
                /// </summary>
                ERROR_BAD_NETPATH = 53,
                /// <summary>
                /// The network is busy.
                /// </summary>
                ERROR_NETWORK_BUSY = 54,
                /// <summary>
                /// The specified network resource or device is no longer available.
                /// </summary>
                ERROR_DEV_NOT_EXIST = 55,
                /// <summary>
                /// The network BIOS command limit has been reached.
                /// </summary>
                ERROR_TOO_MANY_CMDS = 56,
                /// <summary>
                /// A network adapter hardware error occurred.
                /// </summary>
                ERROR_ADAP_HDW_ERR = 57,
                /// <summary>
                /// The specified server cannot perform the requested operation.
                /// </summary>
                ERROR_BAD_NET_RESP = 58,
                /// <summary>
                /// An unexpected network error occurred.
                /// </summary>
                ERROR_UNEXP_NET_ERR = 59,
                /// <summary>
                /// The remote adapter is not compatible.
                /// </summary>
                ERROR_BAD_REM_ADAP = 60,
                /// <summary>
                /// The printer queue is full.
                /// </summary>
                ERROR_PRINTQ_FULL = 61,
                /// <summary>
                /// Space to store the file waiting to be printed is not available on the server.
                /// </summary>
                ERROR_NO_SPOOL_SPACE = 62,
                /// <summary>
                /// Your file waiting to be printed was deleted.
                /// </summary>
                ERROR_PRINT_CANCELLED = 63,
                /// <summary>
                /// The specified network name is no longer available.
                /// </summary>
                ERROR_NETNAME_DELETED = 64,
                /// <summary>
                /// Network access is denied.
                /// </summary>
                ERROR_NETWORK_ACCESS_DENIED = 65,
                /// <summary>
                /// The network resource type is not correct.
                /// </summary>
                ERROR_BAD_DEV_TYPE = 66,
                /// <summary>
                /// The network name cannot be found.
                /// </summary>
                ERROR_BAD_NET_NAME = 67,
                /// <summary>
                /// The name limit for the local computer network adapter card was exceeded.
                /// </summary>
                ERROR_TOO_MANY_NAMES = 68,
                /// <summary>
                /// The network BIOS session limit was exceeded.
                /// </summary>
                ERROR_TOO_MANY_SESS = 69,
                /// <summary>
                /// The remote server has been paused or is in the process of being started.
                /// </summary>
                ERROR_SHARING_PAUSED = 70,
                /// <summary>
                /// No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept.
                /// </summary>
                ERROR_REQ_NOT_ACCEP = 71,
                /// <summary>
                /// The specified printer or disk device has been paused.
                /// </summary>
                ERROR_REDIR_PAUSED = 72,
                /// <summary>
                /// The file exists.
                /// </summary>
                ERROR_FILE_EXISTS = 80,
                /// <summary>
                /// The directory or file cannot be created.
                /// </summary>
                ERROR_CANNOT_MAKE = 82,
                /// <summary>
                /// Fail on INT 24.
                /// </summary>
                ERROR_FAIL_I24 = 83,
                /// <summary>
                /// Storage to process this request is not available.
                /// </summary>
                ERROR_OUT_OF_STRUCTURES = 84,
                /// <summary>
                /// The local device name is already in use.
                /// </summary>
                ERROR_ALREADY_ASSIGNED = 85,
                /// <summary>
                /// The specified network password is not correct.
                /// </summary>
                ERROR_INVALID_PASSWORD = 86,
                /// <summary>
                /// The parameter is incorrect.
                /// </summary>
                ERROR_INVALID_PARAMETER = 87,
                /// <summary>
                /// A write fault occurred on the network.
                /// </summary>
                ERROR_NET_WRITE_FAULT = 88,
                /// <summary>
                /// The system cannot start another process at this time.
                /// </summary>
                ERROR_NO_PROC_SLOTS = 89,
                /// <summary>
                /// Cannot create another system semaphore.
                /// </summary>
                ERROR_TOO_MANY_SEMAPHORES = 100,
                /// <summary>
                /// The exclusive semaphore is owned by another process.
                /// </summary>
                ERROR_EXCL_SEM_ALREADY_OWNED = 101,
                /// <summary>
                /// The semaphore is set and cannot be closed.
                /// </summary>
                ERROR_SEM_IS_SET = 102,
                /// <summary>
                /// The semaphore cannot be set again.
                /// </summary>
                ERROR_TOO_MANY_SEM_REQUESTS = 103,
                /// <summary>
                /// Cannot request exclusive semaphores at interrupt time.
                /// </summary>
                ERROR_INVALID_AT_INTERRUPT_TIME = 104,
                /// <summary>
                /// The previous ownership of this semaphore has ended.
                /// </summary>
                ERROR_SEM_OWNER_DIED = 105,
                /// <summary>
                /// Insert the diskette for drive %1.
                /// </summary>
                ERROR_SEM_USER_LIMIT = 106,
                /// <summary>
                /// The program stopped because an alternate diskette was not inserted.
                /// </summary>
                ERROR_DISK_CHANGE = 107,
                /// <summary>
                /// The disk is in use or locked by another process.
                /// </summary>
                ERROR_DRIVE_LOCKED = 108,
                /// <summary>
                /// The pipe has been ended.
                /// </summary>
                ERROR_BROKEN_PIPE = 109,
                /// <summary>
                /// The system cannot open the device or file specified.
                /// </summary>
                ERROR_OPEN_FAILED = 110,
                /// <summary>
                /// The file name is too long.
                /// </summary>
                ERROR_BUFFER_OVERFLOW = 111,
                /// <summary>
                /// There is not enough space on the disk.
                /// </summary>
                ERROR_DISK_FULL = 112,
                /// <summary>
                /// No more internal file identifiers available.
                /// </summary>
                ERROR_NO_MORE_SEARCH_HANDLES = 113,
                /// <summary>
                /// The target internal file identifier is incorrect.
                /// </summary>
                ERROR_INVALID_TARGET_HANDLE = 114,
                /// <summary>
                /// The IOCTL call made by the application program is not correct.
                /// </summary>
                ERROR_INVALID_CATEGORY = 117,
                /// <summary>
                /// The verify-on-write switch parameter value is not correct.
                /// </summary>
                ERROR_INVALID_VERIFY_SWITCH = 118,
                /// <summary>
                /// The system does not support the command requested.
                /// </summary>
                ERROR_BAD_DRIVER_LEVEL = 119,
                /// <summary>
                /// This function is not supported on this system.
                /// </summary>
                ERROR_CALL_NOT_IMPLEMENTED = 120,
                /// <summary>
                /// The semaphore timeout period has expired.
                /// </summary>
                ERROR_SEM_TIMEOUT = 121,
                /// <summary>
                /// The data area passed to a system call is too small.
                /// </summary>
                ERROR_INSUFFICIENT_BUFFER = 122,
                /// <summary>
                /// The filename, directory name, or volume label syntax is incorrect.
                /// </summary>
                ERROR_INVALID_NAME = 123,
                /// <summary>
                /// The system call level is not correct.
                /// </summary>
                ERROR_INVALID_LEVEL = 124,
                /// <summary>
                /// The disk has no volume label.
                /// </summary>
                ERROR_NO_VOLUME_LABEL = 125,
                /// <summary>
                /// The specified module could not be found.
                /// </summary>
                ERROR_MOD_NOT_FOUND = 126,
                /// <summary>
                /// The specified procedure could not be found.
                /// </summary>
                ERROR_PROC_NOT_FOUND = 127,
                /// <summary>
                /// There are no child processes to wait for.
                /// </summary>
                ERROR_WAIT_NO_CHILDREN = 128,
                /// <summary>
                /// The %1 application cannot be run in Win32 mode.
                /// </summary>
                ERROR_CHILD_NOT_COMPLETE = 129,
                /// <summary>
                /// Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O.
                /// </summary>
                ERROR_DIRECT_ACCESS_HANDLE = 130,
                /// <summary>
                /// An attempt was made to move the file pointer before the beginning of the file.
                /// </summary>
                ERROR_NEGATIVE_SEEK = 131,
                /// <summary>
                /// The file pointer cannot be set on the specified device or file.
                /// </summary>
                ERROR_SEEK_ON_DEVICE = 132,
                /// <summary>
                /// A JOIN or SUBST command cannot be used for a drive that contains previously joined drives.
                /// </summary>
                ERROR_IS_JOIN_TARGET = 133,
                /// <summary>
                /// An attempt was made to use a JOIN or SUBST command on a drive that has already been joined.
                /// </summary>
                ERROR_IS_JOINED = 134,
                /// <summary>
                /// An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted.
                /// </summary>
                ERROR_IS_SUBSTED = 135,
                /// <summary>
                /// The system tried to delete the JOIN of a drive that is not joined.
                /// </summary>
                ERROR_NOT_JOINED = 136,
                /// <summary>
                /// The system tried to delete the substitution of a drive that is not substituted.
                /// </summary>
                ERROR_NOT_SUBSTED = 137,
                /// <summary>
                /// The system tried to join a drive to a directory on a joined drive.
                /// </summary>
                ERROR_JOIN_TO_JOIN = 138,
                /// <summary>
                /// The system tried to substitute a drive to a directory on a substituted drive.
                /// </summary>
                ERROR_SUBST_TO_SUBST = 139,
                /// <summary>
                /// The system tried to join a drive to a directory on a substituted drive.
                /// </summary>
                ERROR_JOIN_TO_SUBST = 140,
                /// <summary>
                /// The system tried to SUBST a drive to a directory on a joined drive.
                /// </summary>
                ERROR_SUBST_TO_JOIN = 141,
                /// <summary>
                /// The system cannot perform a JOIN or SUBST at this time.
                /// </summary>
                ERROR_BUSY_DRIVE = 142,
                /// <summary>
                /// The system cannot join or substitute a drive to or for a directory on the same drive.
                /// </summary>
                ERROR_SAME_DRIVE = 143,
                /// <summary>
                /// The directory is not a subdirectory of the root directory.
                /// </summary>
                ERROR_DIR_NOT_ROOT = 144,
                /// <summary>
                /// The directory is not empty.
                /// </summary>
                ERROR_DIR_NOT_EMPTY = 145,
                /// <summary>
                /// The path specified is being used in a substitute.
                /// </summary>
                ERROR_IS_SUBST_PATH = 146,
                /// <summary>
                /// Not enough resources are available to process this command.
                /// </summary>
                ERROR_IS_JOIN_PATH = 147,
                /// <summary>
                /// The path specified cannot be used at this time.
                /// </summary>
                ERROR_PATH_BUSY = 148,
                /// <summary>
                /// An attempt was made to join or substitute a drive for which a directory on the drive is the target of a previous substitute.
                /// </summary>
                ERROR_IS_SUBST_TARGET = 149,
                /// <summary>
                /// System trace information was not specified in your CONFIG.SYS file, or tracing is disallowed.
                /// </summary>
                ERROR_SYSTEM_TRACE = 150,
                /// <summary>
                /// The number of specified semaphore events for DosMuxSemWait is not correct.
                /// </summary>
                ERROR_INVALID_EVENT_COUNT = 151,
                /// <summary>
                /// DosMuxSemWait did not execute; too many semaphores are already set.
                /// </summary>
                ERROR_TOO_MANY_MUXWAITERS = 152,
                /// <summary>
                /// The DosMuxSemWait list is not correct.
                /// </summary>
                ERROR_INVALID_LIST_FORMAT = 153,
                /// <summary>
                /// The volume label you entered exceeds the label character limit of the target file system.
                /// </summary>
                ERROR_LABEL_TOO_LONG = 154,
                /// <summary>
                /// Cannot create another thread.
                /// </summary>
                ERROR_TOO_MANY_TCBS = 155,
                /// <summary>
                /// The recipient process has refused the signal.
                /// </summary>
                ERROR_SIGNAL_REFUSED = 156,
                /// <summary>
                /// The segment is already discarded and cannot be locked.
                /// </summary>
                ERROR_DISCARDED = 157,
                /// <summary>
                /// The segment is already unlocked.
                /// </summary>
                ERROR_NOT_LOCKED = 158,
                /// <summary>
                /// The address for the thread ID is not correct.
                /// </summary>
                ERROR_BAD_THREADID_ADDR = 159,
                /// <summary>
                /// One or more arguments are not correct.
                /// </summary>
                ERROR_BAD_ARGUMENTS = 160,
                /// <summary>
                /// The specified path is invalid.
                /// </summary>
                ERROR_BAD_PATHNAME = 161,
                /// <summary>
                /// A signal is already pending.
                /// </summary>
                ERROR_SIGNAL_PENDING = 162,
                /// <summary>
                /// No more threads can be created in the system.
                /// </summary>
                ERROR_MAX_THRDS_REACHED = 164,
                /// <summary>
                /// Unable to lock a region of a file.
                /// </summary>
                ERROR_LOCK_FAILED = 167,
                /// <summary>
                /// The requested resource is in use.
                /// </summary>
                ERROR_BUSY = 170,
                /// <summary>
                /// Device's command support detection is in progress.
                /// </summary>
                ERROR_DEVICE_SUPPORT_IN_PROGRESS = 171,
                /// <summary>
                /// A lock request was not outstanding for the supplied cancel region.
                /// </summary>
                ERROR_CANCEL_VIOLATION = 173,
                /// <summary>
                /// The file system does not support atomic changes to the lock type.
                /// </summary>
                ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174,
                /// <summary>
                /// The system detected a segment number that was not correct.
                /// </summary>
                ERROR_INVALID_SEGMENT_NUMBER = 180,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_ORDINAL = 182,
                /// <summary>
                /// Cannot create a file when that file already exists.
                /// </summary>
                ERROR_ALREADY_EXISTS = 183,
                /// <summary>
                /// The flag passed is not correct.
                /// </summary>
                ERROR_INVALID_FLAG_NUMBER = 186,
                /// <summary>
                /// The specified system semaphore name was not found.
                /// </summary>
                ERROR_SEM_NOT_FOUND = 187,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_STARTING_CODESEG = 188,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_STACKSEG = 189,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_MODULETYPE = 190,
                /// <summary>
                /// Cannot run %1 in Win32 mode.
                /// </summary>
                ERROR_INVALID_EXE_SIGNATURE = 191,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_EXE_MARKED_INVALID = 192,
                /// <summary>
                /// %1 is not a valid Win32 application.
                /// </summary>
                ERROR_BAD_EXE_FORMAT = 193,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_ITERATED_DATA_EXCEEDS_64k = 194,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_MINALLOCSIZE = 195,
                /// <summary>
                /// The operating system cannot run this application program.
                /// </summary>
                ERROR_DYNLINK_FROM_INVALID_RING = 196,
                /// <summary>
                /// The operating system is not presently configured to run this application.
                /// </summary>
                ERROR_IOPL_NOT_ENABLED = 197,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INVALID_SEGDPL = 198,
                /// <summary>
                /// The operating system cannot run this application program.
                /// </summary>
                ERROR_AUTODATASEG_EXCEEDS_64k = 199,
                /// <summary>
                /// The code segment cannot be greater than or equal to 64K.
                /// </summary>
                ERROR_RING2SEG_MUST_BE_MOVABLE = 200,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201,
                /// <summary>
                /// The operating system cannot run %1.
                /// </summary>
                ERROR_INFLOOP_IN_RELOC_CHAIN = 202,
                /// <summary>
                /// The system could not find the environment option that was entered.
                /// </summary>
                ERROR_ENVVAR_NOT_FOUND = 203,
                /// <summary>
                /// No process in the command subtree has a signal handler.
                /// </summary>
                ERROR_NO_SIGNAL_SENT = 205,
                /// <summary>
                /// The filename or extension is too long.
                /// </summary>
                ERROR_FILENAME_EXCED_RANGE = 206,
                /// <summary>
                /// The ring 2 stack is in use.
                /// </summary>
                ERROR_RING2_STACK_IN_USE = 207,
                /// <summary>
                /// The global filename characters, * or ?, are entered incorrectly or too many global filename characters are specified.
                /// </summary>
                ERROR_META_EXPANSION_TOO_LONG = 208,
                /// <summary>
                /// The signal being posted is not correct.
                /// </summary>
                ERROR_INVALID_SIGNAL_NUMBER = 209,
                /// <summary>
                /// The signal handler cannot be set.
                /// </summary>
                ERROR_THREAD_1_INACTIVE = 210,
                /// <summary>
                /// The segment is locked and cannot be reallocated.
                /// </summary>
                ERROR_LOCKED = 212,
                /// <summary>
                /// Too many dynamic-link modules are attached to this program or dynamic-link module.
                /// </summary>
                ERROR_TOO_MANY_MODULES = 214,
                /// <summary>
                /// Cannot nest calls to LoadModule.
                /// </summary>
                ERROR_NESTING_NOT_ALLOWED = 215,
                /// <summary>
                /// This version of %1 is not compatible with the version of Windows you're running. Check your computer's system information and then contact the software publisher.
                /// </summary>
                ERROR_EXE_MACHINE_TYPE_MISMATCH = 216,
                /// <summary>
                /// The image file %1 is signed, unable to modify.
                /// </summary>
                ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 217,
                /// <summary>
                /// The image file %1 is strong signed, unable to modify.
                /// </summary>
                ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 218,
                /// <summary>
                /// This file is checked out or locked for editing by another user.
                /// </summary>
                ERROR_FILE_CHECKED_OUT = 220,
                /// <summary>
                /// The file must be checked out before saving changes.
                /// </summary>
                ERROR_CHECKOUT_REQUIRED = 221,
                /// <summary>
                /// The file type being saved or retrieved has been blocked.
                /// </summary>
                ERROR_BAD_FILE_TYPE = 222,
                /// <summary>
                /// The file size exceeds the limit allowed and cannot be saved.
                /// </summary>
                ERROR_FILE_TOO_LARGE = 223,
                /// <summary>
                /// Access Denied. Before opening files in this location, you must first add the web site to your trusted sites list, browse to the web site, and select the option to login automatically.
                /// </summary>
                ERROR_FORMS_AUTH_REQUIRED = 224,
                /// <summary>
                /// Operation did not complete successfully because the file contains a virus or potentially unwanted software.
                /// </summary>
                ERROR_VIRUS_INFECTED = 225,
                /// <summary>
                /// This file contains a virus or potentially unwanted software and cannot be opened. Due to the nature of this virus or potentially unwanted software, the file has been removed from this location.
                /// </summary>
                ERROR_VIRUS_DELETED = 226,
                /// <summary>
                /// The pipe is local.
                /// </summary>
                ERROR_PIPE_LOCAL = 229,
                /// <summary>
                /// The pipe state is invalid.
                /// </summary>
                ERROR_BAD_PIPE = 230,
                /// <summary>
                /// All pipe instances are busy.
                /// </summary>
                ERROR_PIPE_BUSY = 231,
                /// <summary>
                /// The pipe is being closed.
                /// </summary>
                ERROR_NO_DATA = 232,
                /// <summary>
                /// No process is on the other end of the pipe.
                /// </summary>
                ERROR_PIPE_NOT_CONNECTED = 233,
                /// <summary>
                /// More data is available.
                /// </summary>
                ERROR_MORE_DATA = 234,
                /// <summary>
                /// The session was canceled.
                /// </summary>
                ERROR_VC_DISCONNECTED = 240,
                /// <summary>
                /// The specified extended attribute name was invalid.
                /// </summary>
                ERROR_INVALID_EA_NAME = 254,
                /// <summary>
                /// The extended attributes are inconsistent.
                /// </summary>
                ERROR_EA_LIST_INCONSISTENT = 255,
                /// <summary>
                /// The wait operation timed out.
                /// </summary>
                WAIT_TIMEOUT = 258,
                /// <summary>
                /// No more data is available.
                /// </summary>
                ERROR_NO_MORE_ITEMS = 259,
                /// <summary>
                /// The copy functions cannot be used.
                /// </summary>
                ERROR_CANNOT_COPY = 266,
                /// <summary>
                /// The directory name is invalid.
                /// </summary>
                ERROR_DIRECTORY = 267,
                /// <summary>
                /// The extended attributes did not fit in the buffer.
                /// </summary>
                ERROR_EAS_DIDNT_FIT = 275,
                /// <summary>
                /// The extended attribute file on the mounted file system is corrupt.
                /// </summary>
                ERROR_EA_FILE_CORRUPT = 276,
                /// <summary>
                /// The extended attribute table file is full.
                /// </summary>
                ERROR_EA_TABLE_FULL = 277,
                /// <summary>
                /// The specified extended attribute handle is invalid.
                /// </summary>
                ERROR_INVALID_EA_HANDLE = 278,
                /// <summary>
                /// The mounted file system does not support extended attributes.
                /// </summary>
                ERROR_EAS_NOT_SUPPORTED = 282,
                /// <summary>
                /// Attempt to release mutex not owned by caller.
                /// </summary>
                ERROR_NOT_OWNER = 288,
                /// <summary>
                /// Too many posts were made to a semaphore.
                /// </summary>
                ERROR_TOO_MANY_POSTS = 298,
                /// <summary>
                /// Only part of a ReadProcessMemory or WriteProcessMemory request was completed.
                /// </summary>
                ERROR_PARTIAL_COPY = 299,
                /// <summary>
                /// The oplock request is denied.
                /// </summary>
                ERROR_OPLOCK_NOT_GRANTED = 300,
                /// <summary>
                /// An invalid oplock acknowledgment was received by the system.
                /// </summary>
                ERROR_INVALID_OPLOCK_PROTOCOL = 301,
                /// <summary>
                /// The volume is too fragmented to complete this operation.
                /// </summary>
                ERROR_DISK_TOO_FRAGMENTED = 302,
                /// <summary>
                /// The file cannot be opened because it is in the process of being deleted.
                /// </summary>
                ERROR_DELETE_PENDING = 303,
                /// <summary>
                /// Short name settings may not be changed on this volume due to the global registry setting.
                /// </summary>
                ERROR_INCOMPATIBLE_WITH_GLOBAL_SHORT_NAME_REGISTRY_SETTING = 304,
                /// <summary>
                /// Short names are not enabled on this volume.
                /// </summary>
                ERROR_SHORT_NAMES_NOT_ENABLED_ON_VOLUME = 305,
                /// <summary>
                /// The security stream for the given volume is in an inconsistent state. Please run CHKDSK on the volume.
                /// </summary>
                ERROR_SECURITY_STREAM_IS_INCONSISTENT = 306,
                /// <summary>
                /// A requested file lock operation cannot be processed due to an invalid byte range.
                /// </summary>
                ERROR_INVALID_LOCK_RANGE = 307,
                /// <summary>
                /// The subsystem needed to support the image type is not present.
                /// </summary>
                ERROR_IMAGE_SUBSYSTEM_NOT_PRESENT = 308,
                /// <summary>
                /// The specified file already has a notification GUID associated with it.
                /// </summary>
                ERROR_NOTIFICATION_GUID_ALREADY_DEFINED = 309,
                /// <summary>
                /// An invalid exception handler routine has been detected.
                /// </summary>
                ERROR_INVALID_EXCEPTION_HANDLER = 310,
                /// <summary>
                /// Duplicate privileges were specified for the token.
                /// </summary>
                ERROR_DUPLICATE_PRIVILEGES = 311,
                /// <summary>
                /// No ranges for the specified operation were able to be processed.
                /// </summary>
                ERROR_NO_RANGES_PROCESSED = 312,
                /// <summary>
                /// Operation is not allowed on a file system internal file.
                /// </summary>
                ERROR_NOT_ALLOWED_ON_SYSTEM_FILE = 313,
                /// <summary>
                /// The physical resources of this disk have been exhausted.
                /// </summary>
                ERROR_DISK_RESOURCES_EXHAUSTED = 314,
                /// <summary>
                /// The token representing the data is invalid.
                /// </summary>
                ERROR_INVALID_TOKEN = 315,
                /// <summary>
                /// The device does not support the command feature.
                /// </summary>
                ERROR_DEVICE_FEATURE_NOT_SUPPORTED = 316,
                /// <summary>
                /// The system cannot find message text for message number 0x%1 in the message file for %2.
                /// </summary>
                ERROR_MR_MID_NOT_FOUND = 317,
                /// <summary>
                /// The scope specified was not found.
                /// </summary>
                ERROR_SCOPE_NOT_FOUND = 318,
                /// <summary>
                /// The Central Access Policy specified is not defined on the target machine.
                /// </summary>
                ERROR_UNDEFINED_SCOPE = 319,
                /// <summary>
                /// The Central Access Policy obtained from Active Directory is invalid.
                /// </summary>
                ERROR_INVALID_CAP = 320,
                /// <summary>
                /// The device is unreachable.
                /// </summary>
                ERROR_DEVICE_UNREACHABLE = 321,
                /// <summary>
                /// The target device has insufficient resources to complete the operation.
                /// </summary>
                ERROR_DEVICE_NO_RESOURCES = 322,
                /// <summary>
                /// A data integrity checksum error occurred. Data in the file stream is corrupt.
                /// </summary>
                ERROR_DATA_CHECKSUM_ERROR = 323,
                /// <summary>
                /// An attempt was made to modify both a KERNEL and normal Extended Attribute (EA) in the same operation.
                /// </summary>
                ERROR_INTERMIXED_KERNEL_EA_OPERATION = 324,
                /// <summary>
                /// Device does not support file-level TRIM.
                /// </summary>
                ERROR_FILE_LEVEL_TRIM_NOT_SUPPORTED = 326,
                /// <summary>
                /// The command specified a data offset that does not align to the device's granularity/alignment.
                /// </summary>
                ERROR_OFFSET_ALIGNMENT_VIOLATION = 327,
                /// <summary>
                /// The command specified an invalid field in its parameter list.
                /// </summary>
                ERROR_INVALID_FIELD_IN_PARAMETER_LIST = 328,
                /// <summary>
                /// An operation is currently in progress with the device.
                /// </summary>
                ERROR_OPERATION_IN_PROGRESS = 329,
                /// <summary>
                /// An attempt was made to send down the command via an invalid path to the target device.
                /// </summary>
                ERROR_BAD_DEVICE_PATH = 330,
                /// <summary>
                /// The command specified a number of descriptors that exceeded the maximum supported by the device.
                /// </summary>
                ERROR_TOO_MANY_DESCRIPTORS = 331,
                /// <summary>
                /// Scrub is disabled on the specified file.
                /// </summary>
                ERROR_SCRUB_DATA_DISABLED = 332,
                /// <summary>
                /// The storage device does not provide redundancy.
                /// </summary>
                ERROR_NOT_REDUNDANT_STORAGE = 333,
                /// <summary>
                /// An operation is not supported on a resident file.
                /// </summary>
                ERROR_RESIDENT_FILE_NOT_SUPPORTED = 334,
                /// <summary>
                /// An operation is not supported on a compressed file.
                /// </summary>
                ERROR_COMPRESSED_FILE_NOT_SUPPORTED = 335,
                /// <summary>
                /// An operation is not supported on a directory.
                /// </summary>
                ERROR_DIRECTORY_NOT_SUPPORTED = 336,
                /// <summary>
                /// The specified copy of the requested data could not be read.
                /// </summary>
                ERROR_NOT_READ_FROM_COPY = 337,
                /// <summary>
                /// No action was taken as a system reboot is required.
                /// </summary>
                ERROR_FAIL_NOACTION_REBOOT = 350,
                /// <summary>
                /// The shutdown operation failed.
                /// </summary>
                ERROR_FAIL_SHUTDOWN = 351,
                /// <summary>
                /// The restart operation failed.
                /// </summary>
                ERROR_FAIL_RESTART = 352,
                /// <summary>
                /// The maximum number of sessions has been reached.
                /// </summary>
                ERROR_MAX_SESSIONS_REACHED = 353,
                /// <summary>
                /// The thread is already in background processing mode.
                /// </summary>
                ERROR_THREAD_MODE_ALREADY_BACKGROUND = 400,
                /// <summary>
                /// The thread is not in background processing mode.
                /// </summary>
                ERROR_THREAD_MODE_NOT_BACKGROUND = 401,
                /// <summary>
                /// The process is already in background processing mode.
                /// </summary>
                ERROR_PROCESS_MODE_ALREADY_BACKGROUND = 402,
                /// <summary>
                /// The process is not in background processing mode.
                /// </summary>
                ERROR_PROCESS_MODE_NOT_BACKGROUND = 403,
                /// <summary>
                /// Attempt to access invalid address.
                /// </summary>
                ERROR_INVALID_ADDRESS = 487,
                /// <summary>
                /// User profile cannot be loaded.
                /// </summary>
                ERROR_USER_PROFILE_LOAD = 500,
                /// <summary>
                /// Arithmetic result exceeded 32 bits.
                /// </summary>
                ERROR_ARITHMETIC_OVERFLOW = 534,
                /// <summary>
                /// There is a process on other end of the pipe.
                /// </summary>
                ERROR_PIPE_CONNECTED = 535,
                /// <summary>
                /// Waiting for a process to open the other end of the pipe.
                /// </summary>
                ERROR_PIPE_LISTENING = 536,
                /// <summary>
                /// Application verifier has found an error in the current process.
                /// </summary>
                ERROR_VERIFIER_STOP = 537,
                /// <summary>
                /// An error occurred in the ABIOS subsystem.
                /// </summary>
                ERROR_ABIOS_ERROR = 538,
                /// <summary>
                /// A warning occurred in the WX86 subsystem.
                /// </summary>
                ERROR_WX86_WARNING = 539,
                /// <summary>
                /// An error occurred in the WX86 subsystem.
                /// </summary>
                ERROR_WX86_ERROR = 540,
                /// <summary>
                /// An attempt was made to cancel or set a timer that has an associated APC and the subject thread is not the thread that originally set the timer with an associated APC routine.
                /// </summary>
                ERROR_TIMER_NOT_CANCELED = 541,
                /// <summary>
                /// Unwind exception code.
                /// </summary>
                ERROR_UNWIND = 542,
                /// <summary>
                /// An invalid or unaligned stack was encountered during an unwind operation.
                /// </summary>
                ERROR_BAD_STACK = 543,
                /// <summary>
                /// An invalid unwind target was encountered during an unwind operation.
                /// </summary>
                ERROR_INVALID_UNWIND_TARGET = 544,
                /// <summary>
                /// Invalid Object Attributes specified to NtCreatePort or invalid Port Attributes specified to NtConnectPort
                /// </summary>
                ERROR_INVALID_PORT_ATTRIBUTES = 545,
                /// <summary>
                /// Length of message passed to NtRequestPort or NtRequestWaitReplyPort was longer than the maximum message allowed by the port.
                /// </summary>
                ERROR_PORT_MESSAGE_TOO_LONG = 546,
                /// <summary>
                /// An attempt was made to lower a quota limit below the current usage.
                /// </summary>
                ERROR_INVALID_QUOTA_LOWER = 547,
                /// <summary>
                /// An attempt was made to attach to a device that was already attached to another device.
                /// </summary>
                ERROR_DEVICE_ALREADY_ATTACHED = 548,
                /// <summary>
                /// An attempt was made to execute an instruction at an unaligned address and the host system does not support unaligned instruction references.
                /// </summary>
                ERROR_INSTRUCTION_MISALIGNMENT = 549,
                /// <summary>
                /// Profiling not started.
                /// </summary>
                ERROR_PROFILING_NOT_STARTED = 550,
                /// <summary>
                /// Profiling not stopped.
                /// </summary>
                ERROR_PROFILING_NOT_STOPPED = 551,
                /// <summary>
                /// The passed ACL did not contain the minimum required information.
                /// </summary>
                ERROR_COULD_NOT_INTERPRET = 552,
                /// <summary>
                /// The number of active profiling objects is at the maximum and no more may be started.
                /// </summary>
                ERROR_PROFILING_AT_LIMIT = 553,
                /// <summary>
                /// Used to indicate that an operation cannot continue without blocking for I/O.
                /// </summary>
                ERROR_CANT_WAIT = 554,
                /// <summary>
                /// Indicates that a thread attempted to terminate itself by default (called NtTerminateThread with NULL) and it was the last thread in the current process.
                /// </summary>
                ERROR_CANT_TERMINATE_SELF = 555,
                /// <summary>
                /// If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.
                /// </summary>
                ERROR_UNEXPECTED_MM_CREATE_ERR = 556,
                /// <summary>
                /// If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.
                /// </summary>
                ERROR_UNEXPECTED_MM_MAP_ERROR = 557,
                /// <summary>
                /// If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.
                /// </summary>
                ERROR_UNEXPECTED_MM_EXTEND_ERR = 558,
                /// <summary>
                /// A malformed function table was encountered during an unwind operation.
                /// </summary>
                ERROR_BAD_FUNCTION_TABLE = 559,
                /// <summary>
                /// Indicates that an attempt was made to assign protection to a file system file or directory and one of the SIDs in the security descriptor could not be translated into a GUID that could be stored by the file system. This causes the protection attempt to fail, which may cause a file creation attempt to fail.
                /// </summary>
                ERROR_NO_GUID_TRANSLATION = 560,
                /// <summary>
                /// Indicates that an attempt was made to grow an LDT by setting its size, or that the size was not an even number of selectors.
                /// </summary>
                ERROR_INVALID_LDT_SIZE = 561,
                /// <summary>
                /// Indicates that the starting value for the LDT information was not an integral multiple of the selector size.
                /// </summary>
                ERROR_INVALID_LDT_OFFSET = 563,
                /// <summary>
                /// Indicates that the user supplied an invalid descriptor when trying to set up Ldt descriptors.
                /// </summary>
                ERROR_INVALID_LDT_DESCRIPTOR = 564,
                /// <summary>
                /// Indicates a process has too many threads to perform the requested action. For example, assignment of a primary token may only be performed when a process has zero or one threads.
                /// </summary>
                ERROR_TOO_MANY_THREADS = 565,
                /// <summary>
                /// An attempt was made to operate on a thread within a specific process, but the thread specified is not in the process specified.
                /// </summary>
                ERROR_THREAD_NOT_IN_PROCESS = 566,
                /// <summary>
                /// Page file quota was exceeded.
                /// </summary>
                ERROR_PAGEFILE_QUOTA_EXCEEDED = 567,
                /// <summary>
                /// The Netlogon service cannot start because another Netlogon service running in the domain conflicts with the specified role.
                /// </summary>
                ERROR_LOGON_SERVER_CONFLICT = 568,
                /// <summary>
                /// The SAM database on a Windows Server is significantly out of synchronization with the copy on the Domain Controller. A complete synchronization is required.
                /// </summary>
                ERROR_SYNCHRONIZATION_REQUIRED = 569,
                /// <summary>
                /// The NtCreateFile API failed. This error should never be returned to an application, it is a place holder for the Windows Lan Manager Redirector to use in its internal error mapping routines.
                /// </summary>
                ERROR_NET_OPEN_FAILED = 570,
                /// <summary>
                /// {Privilege Failed} The I/O permissions for the process could not be changed.
                /// </summary>
                ERROR_IO_PRIVILEGE_FAILED = 571,
                /// <summary>
                /// {Application Exit by CTRL+C} The application terminated as a result of a CTRL+C.
                /// </summary>
                ERROR_CONTROL_C_EXIT = 572,
                /// <summary>
                /// {Missing System File} The required system file %hs is bad or missing.
                /// </summary>
                ERROR_MISSING_SYSTEMFILE = 573,
                /// <summary>
                /// {Application Error} The exception %s (0x%08lx) occurred in the application at location 0x%08lx.
                /// </summary>
                ERROR_UNHANDLED_EXCEPTION = 574,
                /// <summary>
                /// {Application Error} The application was unable to start correctly (0x%lx). Click OK to close the application.
                /// </summary>
                ERROR_APP_INIT_FAILURE = 575,
                /// <summary>
                /// {Unable to Create Paging File} The creation of the paging file %hs failed (%lx). The requested size was %ld.
                /// </summary>
                ERROR_PAGEFILE_CREATE_FAILED = 576,
                /// <summary>
                /// Windows cannot verify the digital signature for this file. A recent hardware or software change might have installed a file that is signed incorrectly or damaged, or that might be malicious software from an unknown source.
                /// </summary>
                ERROR_INVALID_IMAGE_HASH = 577,
                /// <summary>
                /// {No Paging File Specified} No paging file was specified in the system configuration.
                /// </summary>
                ERROR_NO_PAGEFILE = 578,
                /// <summary>
                /// {EXCEPTION} A real-mode application issued a floating-point instruction and floating-point hardware is not present.
                /// </summary>
                ERROR_ILLEGAL_FLOAT_CONTEXT = 579,
                /// <summary>
                /// An event pair synchronization operation was performed using the thread specific client/server event pair object, but no event pair object was associated with the thread.
                /// </summary>
                ERROR_NO_EVENT_PAIR = 580,
                /// <summary>
                /// A Windows Server has an incorrect configuration.
                /// </summary>
                ERROR_DOMAIN_CTRLR_CONFIG_ERROR = 581,
                /// <summary>
                /// An illegal character was encountered. For a multi-byte character set this includes a lead byte without a succeeding trail byte. For the Unicode character set this includes the characters 0xFFFF and 0xFFFE.
                /// </summary>
                ERROR_ILLEGAL_CHARACTER = 582,
                /// <summary>
                /// The Unicode character is not defined in the Unicode character set installed on the system.
                /// </summary>
                ERROR_UNDEFINED_CHARACTER = 583,
                /// <summary>
                /// The paging file cannot be created on a floppy diskette.
                /// </summary>
                ERROR_FLOPPY_VOLUME = 584,
                /// <summary>
                /// The system BIOS failed to connect a system interrupt to the device or bus for which the device is connected.
                /// </summary>
                ERROR_BIOS_FAILED_TO_CONNECT_INTERRUPT = 585,
                /// <summary>
                /// This operation is only allowed for the Primary Domain Controller of the domain.
                /// </summary>
                ERROR_BACKUP_CONTROLLER = 586,
                /// <summary>
                /// An attempt was made to acquire a mutant such that its maximum count would have been exceeded.
                /// </summary>
                ERROR_MUTANT_LIMIT_EXCEEDED = 587,
                /// <summary>
                /// A volume has been accessed for which a file system driver is required that has not yet been loaded.
                /// </summary>
                ERROR_FS_DRIVER_REQUIRED = 588,
                /// <summary>
                /// {Registry File Failure} The registry cannot load the hive (file): %hs or its log or alternate. It is corrupt, absent, or not writable.
                /// </summary>
                ERROR_CANNOT_LOAD_REGISTRY_FILE = 589,
                /// <summary>
                /// {Unexpected Failure in DebugActiveProcess} An unexpected failure occurred while processing a DebugActiveProcess API request. You may choose OK to terminate the process, or Cancel to ignore the error.
                /// </summary>
                ERROR_DEBUG_ATTACH_FAILED = 590,
                /// <summary>
                /// {Fatal System Error} The %hs system process terminated unexpectedly with a status of 0x%08x (0x%08x 0x%08x). The system has been shut down.
                /// </summary>
                ERROR_SYSTEM_PROCESS_TERMINATED = 591,
                /// <summary>
                /// {Data Not Accepted} The TDI client could not handle the data received during an indication.
                /// </summary>
                ERROR_DATA_NOT_ACCEPTED = 592,
                /// <summary>
                /// NTVDM encountered a hard error.
                /// </summary>
                ERROR_VDM_HARD_ERROR = 593,
                /// <summary>
                /// {Cancel Timeout} The driver %hs failed to complete a cancelled I/O request in the allotted time.
                /// </summary>
                ERROR_DRIVER_CANCEL_TIMEOUT = 594,
                /// <summary>
                /// {Reply Message Mismatch} An attempt was made to reply to an LPC message, but the thread specified by the client ID in the message was not waiting on that message.
                /// </summary>
                ERROR_REPLY_MESSAGE_MISMATCH = 595,
                /// <summary>
                /// {Delayed Write Failed} Windows was unable to save all the data for the file %hs. The data has been lost. This error may be caused by a failure of your computer hardware or network connection. Please try to save this file elsewhere.
                /// </summary>
                ERROR_LOST_WRITEBEHIND_DATA = 596,
                /// <summary>
                /// The parameter(s) passed to the server in the client/server shared memory window were invalid. Too much data may have been put in the shared memory window.
                /// </summary>
                ERROR_CLIENT_SERVER_PARAMETERS_INVALID = 597,
                /// <summary>
                /// The stream is not a tiny stream.
                /// </summary>
                ERROR_NOT_TINY_STREAM = 598,
                /// <summary>
                /// The request must be handled by the stack overflow code.
                /// </summary>
                ERROR_STACK_OVERFLOW_READ = 599,
                /// <summary>
                /// Internal OFS status codes indicating how an allocation operation is handled. Either it is retried after the containing onode is moved or the extent stream is converted to a large stream.
                /// </summary>
                ERROR_CONVERT_TO_LARGE = 600,
                /// <summary>
                /// The attempt to find the object found an object matching by ID on the volume but it is out of the scope of the handle used for the operation.
                /// </summary>
                ERROR_FOUND_OUT_OF_SCOPE = 601,
                /// <summary>
                /// The bucket array must be grown. Retry transaction after doing so.
                /// </summary>
                ERROR_ALLOCATE_BUCKET = 602,
                /// <summary>
                /// The user/kernel marshalling buffer has overflowed.
                /// </summary>
                ERROR_MARSHALL_OVERFLOW = 603,
                /// <summary>
                /// The supplied variant structure contains invalid data.
                /// </summary>
                ERROR_INVALID_VARIANT = 604,
                /// <summary>
                /// The specified buffer contains ill-formed data.
                /// </summary>
                ERROR_BAD_COMPRESSION_BUFFER = 605,
                /// <summary>
                /// {Audit Failed} An attempt to generate a security audit failed.
                /// </summary>
                ERROR_AUDIT_FAILED = 606,
                /// <summary>
                /// The timer resolution was not previously set by the current process.
                /// </summary>
                ERROR_TIMER_RESOLUTION_NOT_SET = 607,
                /// <summary>
                /// There is insufficient account information to log you on.
                /// </summary>
                ERROR_INSUFFICIENT_LOGON_INFO = 608,
                /// <summary>
                /// {Invalid DLL Entrypoint} The dynamic link library %hs is not written correctly. The stack pointer has been left in an inconsistent state. The entrypoint should be declared as WINAPI or STDCALL. Select YES to fail the DLL load. Select NO to continue execution. Selecting NO may cause the application to operate incorrectly.
                /// </summary>
                ERROR_BAD_DLL_ENTRYPOINT = 609,
                /// <summary>
                /// {Invalid Service Callback Entrypoint} The %hs service is not written correctly. The stack pointer has been left in an inconsistent state. The callback entrypoint should be declared as WINAPI or STDCALL. Selecting OK will cause the service to continue operation. However, the service process may operate incorrectly.
                /// </summary>
                ERROR_BAD_SERVICE_ENTRYPOINT = 610,
                /// <summary>
                /// There is an IP address conflict with another system on the network.
                /// </summary>
                ERROR_IP_ADDRESS_CONFLICT1 = 611,
                /// <summary>
                /// There is an IP address conflict with another system on the network.
                /// </summary>
                ERROR_IP_ADDRESS_CONFLICT2 = 612,
                /// <summary>
                /// {Low On Registry Space} The system has reached the maximum size allowed for the system part of the registry. Additional storage requests will be ignored.
                /// </summary>
                ERROR_REGISTRY_QUOTA_LIMIT = 613,
                /// <summary>
                /// A callback return system service cannot be executed when no callback is active.
                /// </summary>
                ERROR_NO_CALLBACK_ACTIVE = 614,
                /// <summary>
                /// The password provided is too short to meet the policy of your user account. Please choose a longer password.
                /// </summary>
                ERROR_PWD_TOO_SHORT = 615,
                /// <summary>
                /// The policy of your user account does not allow you to change passwords too frequently. This is done to prevent users from changing back to a familiar, but potentially discovered, password. If you feel your password has been compromised then please contact your administrator immediately to have a new one assigned.
                /// </summary>
                ERROR_PWD_TOO_RECENT = 616,
                /// <summary>
                /// You have attempted to change your password to one that you have used in the past. The policy of your user account does not allow this. Please select a password that you have not previously used.
                /// </summary>
                ERROR_PWD_HISTORY_CONFLICT = 617,
                /// <summary>
                /// The specified compression format is unsupported.
                /// </summary>
                ERROR_UNSUPPORTED_COMPRESSION = 618,
                /// <summary>
                /// The specified hardware profile configuration is invalid.
                /// </summary>
                ERROR_INVALID_HW_PROFILE = 619,
                /// <summary>
                /// The specified Plug and Play registry device path is invalid.
                /// </summary>
                ERROR_INVALID_PLUGPLAY_DEVICE_PATH = 620,
                /// <summary>
                /// The specified quota list is internally inconsistent with its descriptor.
                /// </summary>
                ERROR_QUOTA_LIST_INCONSISTENT = 621,
                /// <summary>
                /// {Windows Evaluation Notification} The evaluation period for this installation of Windows has expired. This system will shutdown in 1 hour. To restore access to this installation of Windows, please upgrade this installation using a licensed distribution of this product.
                /// </summary>
                ERROR_EVALUATION_EXPIRATION = 622,
                /// <summary>
                /// {Illegal System DLL Relocation} The system DLL %hs was relocated in memory. The application will not run properly. The relocation occurred because the DLL %hs occupied an address range reserved for Windows system DLLs. The vendor supplying the DLL should be contacted for a new DLL.
                /// </summary>
                ERROR_ILLEGAL_DLL_RELOCATION = 623,
                /// <summary>
                /// {DLL Initialization Failed} The application failed to initialize because the window station is shutting down.
                /// </summary>
                ERROR_DLL_INIT_FAILED_LOGOFF = 624,
                /// <summary>
                /// The validation process needs to continue on to the next step.
                /// </summary>
                ERROR_VALIDATE_CONTINUE = 625,
                /// <summary>
                /// There are no more matches for the current index enumeration.
                /// </summary>
                ERROR_NO_MORE_MATCHES = 626,
                /// <summary>
                /// The range could not be added to the range list because of a conflict.
                /// </summary>
                ERROR_RANGE_LIST_CONFLICT = 627,
                /// <summary>
                /// The server process is running under a SID different than that required by client.
                /// </summary>
                ERROR_SERVER_SID_MISMATCH = 628,
                /// <summary>
                /// A group marked use for deny only cannot be enabled.
                /// </summary>
                ERROR_CANT_ENABLE_DENY_ONLY = 629,
                /// <summary>
                /// {EXCEPTION} Multiple floating point faults.
                /// </summary>
                ERROR_FLOAT_MULTIPLE_FAULTS = 630,
                /// <summary>
                /// {EXCEPTION} Multiple floating point traps.
                /// </summary>
                ERROR_FLOAT_MULTIPLE_TRAPS = 631,
                /// <summary>
                /// The requested interface is not supported.
                /// </summary>
                ERROR_NOINTERFACE = 632,
                /// <summary>
                /// {System Standby Failed} The driver %hs does not support standby mode. Updating this driver may allow the system to go to standby mode.
                /// </summary>
                ERROR_DRIVER_FAILED_SLEEP = 633,
                /// <summary>
                /// The system file %1 has become corrupt and has been replaced.
                /// </summary>
                ERROR_CORRUPT_SYSTEM_FILE = 634,
                /// <summary>
                /// {Virtual Memory Minimum Too Low} Your system is low on virtual memory. Windows is increasing the size of your virtual memory paging file. During this process, memory requests for some applications may be denied. For more information, see Help.
                /// </summary>
                ERROR_COMMITMENT_MINIMUM = 635,
                /// <summary>
                /// A device was removed so enumeration must be restarted.
                /// </summary>
                ERROR_PNP_RESTART_ENUMERATION = 636,
                /// <summary>
                /// {Fatal System Error} The system image %s is not properly signed. The file has been replaced with the signed file. The system has been shut down.
                /// </summary>
                ERROR_SYSTEM_IMAGE_BAD_SIGNATURE = 637,
                /// <summary>
                /// Device will not start without a reboot.
                /// </summary>
                ERROR_PNP_REBOOT_REQUIRED = 638,
                /// <summary>
                /// There is not enough power to complete the requested operation.
                /// </summary>
                ERROR_INSUFFICIENT_POWER = 639,
                /// <summary>
                /// ERROR_MULTIPLE_FAULT_VIOLATION
                /// </summary>
                ERROR_MULTIPLE_FAULT_VIOLATION = 640,
                /// <summary>
                /// The system is in the process of shutting down.
                /// </summary>
                ERROR_SYSTEM_SHUTDOWN = 641,
                /// <summary>
                /// An attempt to remove a processes DebugPort was made, but a port was not already associated with the process.
                /// </summary>
                ERROR_PORT_NOT_SET = 642,
                /// <summary>
                /// This version of Windows is not compatible with the behavior version of directory forest, domain or domain controller.
                /// </summary>
                ERROR_DS_VERSION_CHECK_FAILURE = 643,
                /// <summary>
                /// The specified range could not be found in the range list.
                /// </summary>
                ERROR_RANGE_NOT_FOUND = 644,
                /// <summary>
                /// The driver was not loaded because the system is booting into safe mode.
                /// </summary>
                ERROR_NOT_SAFE_MODE_DRIVER = 646,
                /// <summary>
                /// The driver was not loaded because it failed its initialization call.
                /// </summary>
                ERROR_FAILED_DRIVER_ENTRY = 647,
                /// <summary>
                /// The \"%hs\" encountered an error while applying power or reading the device configuration. This may be caused by a failure of your hardware or by a poor connection.
                /// </summary>
                ERROR_DEVICE_ENUMERATION_ERROR = 648,
                /// <summary>
                /// The create operation failed because the name contained at least one mount point which resolves to a volume to which the specified device object is not attached.
                /// </summary>
                ERROR_MOUNT_POINT_NOT_RESOLVED = 649,
                /// <summary>
                /// The device object parameter is either not a valid device object or is not attached to the volume specified by the file name.
                /// </summary>
                ERROR_INVALID_DEVICE_OBJECT_PARAMETER = 650,
                /// <summary>
                /// A Machine Check Error has occurred. Please check the system eventlog for additional information.
                /// </summary>
                ERROR_MCA_OCCURED = 651,
                /// <summary>
                /// There was error [%2] processing the driver database.
                /// </summary>
                ERROR_DRIVER_DATABASE_ERROR = 652,
                /// <summary>
                /// System hive size has exceeded its limit.
                /// </summary>
                ERROR_SYSTEM_HIVE_TOO_LARGE = 653,
                /// <summary>
                /// The driver could not be loaded because a previous version of the driver is still in memory.
                /// </summary>
                ERROR_DRIVER_FAILED_PRIOR_UNLOAD = 654,
                /// <summary>
                /// {Volume Shadow Copy Service} Please wait while the Volume Shadow Copy Service prepares volume %hs for hibernation.
                /// </summary>
                ERROR_VOLSNAP_PREPARE_HIBERNATE = 655,
                /// <summary>
                /// The system has failed to hibernate (The error code is %hs). Hibernation will be disabled until the system is restarted.
                /// </summary>
                ERROR_HIBERNATION_FAILURE = 656,
                /// <summary>
                /// The password provided is too long to meet the policy of your user account. Please choose a shorter password.
                /// </summary>
                ERROR_PWD_TOO_LONG = 657,
                /// <summary>
                /// The requested operation could not be completed due to a file system limitation.
                /// </summary>
                ERROR_FILE_SYSTEM_LIMITATION = 665,
                /// <summary>
                /// An assertion failure has occurred.
                /// </summary>
                ERROR_ASSERTION_FAILURE = 668,
                /// <summary>
                /// An error occurred in the ACPI subsystem.
                /// </summary>
                ERROR_ACPI_ERROR = 669,
                /// <summary>
                /// WOW Assertion Error.
                /// </summary>
                ERROR_WOW_ASSERTION = 670,
                /// <summary>
                /// A device is missing in the system BIOS MPS table. This device will not be used. Please contact your system vendor for system BIOS update.
                /// </summary>
                ERROR_PNP_BAD_MPS_TABLE = 671,
                /// <summary>
                /// A translator failed to translate resources.
                /// </summary>
                ERROR_PNP_TRANSLATION_FAILED = 672,
                /// <summary>
                /// A IRQ translator failed to translate resources.
                /// </summary>
                ERROR_PNP_IRQ_TRANSLATION_FAILED = 673,
                /// <summary>
                /// Driver %2 returned invalid ID for a child device (%3).
                /// </summary>
                ERROR_PNP_INVALID_ID = 674,
                /// <summary>
                /// {Kernel Debugger Awakened} the system debugger was awakened by an interrupt.
                /// </summary>
                ERROR_WAKE_SYSTEM_DEBUGGER = 675,
                /// <summary>
                /// {Handles Closed} Handles to objects have been automatically closed as a result of the requested operation.
                /// </summary>
                ERROR_HANDLES_CLOSED = 676,
                /// <summary>
                /// {Too Much Information} The specified access control list (ACL) contained more information than was expected.
                /// </summary>
                ERROR_EXTRANEOUS_INFORMATION = 677,
                /// <summary>
                /// This warning level status indicates that the transaction state already exists for the registry sub-tree, but that a transaction commit was previously aborted. The commit has NOT been completed, but has not been rolled back either (so it may still be committed if desired).
                /// </summary>
                ERROR_RXACT_COMMIT_NECESSARY = 678,
                /// <summary>
                /// {Media Changed} The media may have changed.
                /// </summary>
                ERROR_MEDIA_CHECK = 679,
                /// <summary>
                /// {GUID Substitution} During the translation of a global identifier (GUID) to a Windows security ID (SID), no administratively-defined GUID prefix was found. A substitute prefix was used, which will not compromise system security. However, this may provide a more restrictive access than intended.
                /// </summary>
                ERROR_GUID_SUBSTITUTION_MADE = 680,
                /// <summary>
                /// The create operation stopped after reaching a symbolic link.
                /// </summary>
                ERROR_STOPPED_ON_SYMLINK = 681,
                /// <summary>
                /// A long jump has been executed.
                /// </summary>
                ERROR_LONGJUMP = 682,
                /// <summary>
                /// The Plug and Play query operation was not successful.
                /// </summary>
                ERROR_PLUGPLAY_QUERY_VETOED = 683,
                /// <summary>
                /// A frame consolidation has been executed.
                /// </summary>
                ERROR_UNWIND_CONSOLIDATE = 684,
                /// <summary>
                /// {Registry Hive Recovered} Registry hive (file): %hs was corrupted and it has been recovered. Some data might have been lost.
                /// </summary>
                ERROR_REGISTRY_HIVE_RECOVERED = 685,
                /// <summary>
                /// The application is attempting to run executable code from the module %hs. This may be insecure. An alternative, %hs, is available. Should the application use the secure module %hs?
                /// </summary>
                ERROR_DLL_MIGHT_BE_INSECURE = 686,
                /// <summary>
                /// The application is loading executable code from the module %hs. This is secure, but may be incompatible with previous releases of the operating system. An alternative, %hs, is available. Should the application use the secure module %hs?
                /// </summary>
                ERROR_DLL_MIGHT_BE_INCOMPATIBLE = 687,
                /// <summary>
                /// Debugger did not handle the exception.
                /// </summary>
                ERROR_DBG_EXCEPTION_NOT_HANDLED = 688,
                /// <summary>
                /// Debugger will reply later.
                /// </summary>
                ERROR_DBG_REPLY_LATER = 689,
                /// <summary>
                /// Debugger cannot provide handle.
                /// </summary>
                ERROR_DBG_UNABLE_TO_PROVIDE_HANDLE = 690,
                /// <summary>
                /// Debugger terminated thread.
                /// </summary>
                ERROR_DBG_TERMINATE_THREAD = 691,
                /// <summary>
                /// Debugger terminated process.
                /// </summary>
                ERROR_DBG_TERMINATE_PROCESS = 692,
                /// <summary>
                /// Debugger got control C.
                /// </summary>
                ERROR_DBG_CONTROL_C = 693,
                /// <summary>
                /// Debugger printed exception on control C.
                /// </summary>
                ERROR_DBG_PRINTEXCEPTION_C = 694,
                /// <summary>
                /// Debugger received RIP exception.
                /// </summary>
                ERROR_DBG_RIPEXCEPTION = 695,
                /// <summary>
                /// Debugger received control break.
                /// </summary>
                ERROR_DBG_CONTROL_BREAK = 696,
                /// <summary>
                /// Debugger command communication exception.
                /// </summary>
                ERROR_DBG_COMMAND_EXCEPTION = 697,
                /// <summary>
                /// {Object Exists} An attempt was made to create an object and the object name already existed.
                /// </summary>
                ERROR_OBJECT_NAME_EXISTS = 698,
                /// <summary>
                /// {Thread Suspended} A thread termination occurred while the thread was suspended. The thread was resumed, and termination proceeded.
                /// </summary>
                ERROR_THREAD_WAS_SUSPENDED = 699,
                /// <summary>
                /// {Image Relocated} An image file could not be mapped at the address specified in the image file. Local fixups must be performed on this image.
                /// </summary>
                ERROR_IMAGE_NOT_AT_BASE = 700,
                /// <summary>
                /// This informational level status indicates that a specified registry sub-tree transaction state did not yet exist and had to be created.
                /// </summary>
                ERROR_RXACT_STATE_CREATED = 701,
                /// <summary>
                /// {Segment Load} A virtual DOS machine (VDM) is loading, unloading, or moving an MS-DOS or Win16 program segment image. An exception is raised so a debugger can load, unload or track symbols and breakpoints within these 16-bit segments.
                /// </summary>
                ERROR_SEGMENT_NOTIFICATION = 702,
                /// <summary>
                /// {Invalid Current Directory} The process cannot switch to the startup current directory %hs. Select OK to set current directory to %hs, or select CANCEL to exit.
                /// </summary>
                ERROR_BAD_CURRENT_DIRECTORY = 703,
                /// <summary>
                /// {Redundant Read} To satisfy a read request, the NT fault-tolerant file system successfully read the requested data from a redundant copy. This was done because the file system encountered a failure on a member of the fault-tolerant volume, but was unable to reassign the failing area of the device.
                /// </summary>
                ERROR_FT_READ_RECOVERY_FROM_BACKUP = 704,
                /// <summary>
                /// {Redundant Write} To satisfy a write request, the NT fault-tolerant file system successfully wrote a redundant copy of the information. This was done because the file system encountered a failure on a member of the fault-tolerant volume, but was not able to reassign the failing area of the device.
                /// </summary>
                ERROR_FT_WRITE_RECOVERY = 705,
                /// <summary>
                /// {Machine Type Mismatch} The image file %hs is valid, but is for a machine type other than the current machine. Select OK to continue, or CANCEL to fail the DLL load.
                /// </summary>
                ERROR_IMAGE_MACHINE_TYPE_MISMATCH = 706,
                /// <summary>
                /// {Partial Data Received} The network transport returned partial data to its client. The remaining data will be sent later.
                /// </summary>
                ERROR_RECEIVE_PARTIAL = 707,
                /// <summary>
                /// {Expedited Data Received} The network transport returned data to its client that was marked as expedited by the remote system.
                /// </summary>
                ERROR_RECEIVE_EXPEDITED = 708,
                /// <summary>
                /// {Partial Expedited Data Received} The network transport returned partial data to its client and this data was marked as expedited by the remote system. The remaining data will be sent later.
                /// </summary>
                ERROR_RECEIVE_PARTIAL_EXPEDITED = 709,
                /// <summary>
                /// {TDI Event Done} The TDI indication has completed successfully.
                /// </summary>
                ERROR_EVENT_DONE = 710,
                /// <summary>
                /// {TDI Event Pending} The TDI indication has entered the pending state.
                /// </summary>
                ERROR_EVENT_PENDING = 711,
                /// <summary>
                /// Checking file system on %wZ.
                /// </summary>
                ERROR_CHECKING_FILE_SYSTEM = 712,
                /// <summary>
                /// {Fatal Application Exit} %hs.
                /// </summary>
                ERROR_FATAL_APP_EXIT = 713,
                /// <summary>
                /// The specified registry key is referenced by a predefined handle.
                /// </summary>
                ERROR_PREDEFINED_HANDLE = 714,
                /// <summary>
                /// {Page Unlocked} The page protection of a locked page was changed to 'No Access' and the page was unlocked from memory and from the process.
                /// </summary>
                ERROR_WAS_UNLOCKED = 715,
                /// <summary>
                /// %hs
                /// </summary>
                ERROR_SERVICE_NOTIFICATION = 716,
                /// <summary>
                /// {Page Locked} One of the pages to lock was already locked.
                /// </summary>
                ERROR_WAS_LOCKED = 717,
                /// <summary>
                /// Application popup: %1 : %2
                /// </summary>
                ERROR_LOG_HARD_ERROR = 718,
                /// <summary>
                /// ERROR_ALREADY_WIN32
                /// </summary>
                ERROR_ALREADY_WIN32 = 719,
                /// <summary>
                /// {Machine Type Mismatch} The image file %hs is valid, but is for a machine type other than the current machine.
                /// </summary>
                ERROR_IMAGE_MACHINE_TYPE_MISMATCH_EXE = 720,
                /// <summary>
                /// A yield execution was performed and no thread was available to run.
                /// </summary>
                ERROR_NO_YIELD_PERFORMED = 721,
                /// <summary>
                /// The resumable flag to a timer API was ignored.
                /// </summary>
                ERROR_TIMER_RESUME_IGNORED = 722,
                /// <summary>
                /// The arbiter has deferred arbitration of these resources to its parent.
                /// </summary>
                ERROR_ARBITRATION_UNHANDLED = 723,
                /// <summary>
                /// The inserted CardBus device cannot be started because of a configuration error on \"%hs\".
                /// </summary>
                ERROR_CARDBUS_NOT_SUPPORTED = 724,
                /// <summary>
                /// The CPUs in this multiprocessor system are not all the same revision level. To use all processors the operating system restricts itself to the features of the least capable processor in the system. Should problems occur with this system, contact the CPU manufacturer to see if this mix of processors is supported.
                /// </summary>
                ERROR_MP_PROCESSOR_MISMATCH = 725,
                /// <summary>
                /// The system was put into hibernation.
                /// </summary>
                ERROR_HIBERNATED = 726,
                /// <summary>
                /// The system was resumed from hibernation.
                /// </summary>
                ERROR_RESUME_HIBERNATION = 727,
                /// <summary>
                /// Windows has detected that the system firmware (BIOS) was updated [previous firmware date = %2, current firmware date %3].
                /// </summary>
                ERROR_FIRMWARE_UPDATED = 728,
                /// <summary>
                /// A device driver is leaking locked I/O pages causing system degradation. The system has automatically enabled tracking code in order to try and catch the culprit.
                /// </summary>
                ERROR_DRIVERS_LEAKING_LOCKED_PAGES = 729,
                /// <summary>
                /// The system has awoken.
                /// </summary>
                ERROR_WAKE_SYSTEM = 730,
                /// <summary>
                /// ERROR_WAIT_1
                /// </summary>
                ERROR_WAIT_1 = 731,
                /// <summary>
                /// ERROR_WAIT_2
                /// </summary>
                ERROR_WAIT_2 = 732,
                /// <summary>
                /// ERROR_WAIT_3
                /// </summary>
                ERROR_WAIT_3 = 733,
                /// <summary>
                /// ERROR_WAIT_63
                /// </summary>
                ERROR_WAIT_63 = 734,
                /// <summary>
                /// ERROR_ABANDONED_WAIT_0
                /// </summary>
                ERROR_ABANDONED_WAIT_0 = 735,
                /// <summary>
                /// ERROR_ABANDONED_WAIT_63
                /// </summary>
                ERROR_ABANDONED_WAIT_63 = 736,
                /// <summary>
                /// ERROR_USER_APC
                /// </summary>
                ERROR_USER_APC = 737,
                /// <summary>
                /// ERROR_KERNEL_APC
                /// </summary>
                ERROR_KERNEL_APC = 738,
                /// <summary>
                /// ERROR_ALERTED
                /// </summary>
                ERROR_ALERTED = 739,
                /// <summary>
                /// The requested operation requires elevation.
                /// </summary>
                ERROR_ELEVATION_REQUIRED = 740,
                /// <summary>
                /// A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.
                /// </summary>
                ERROR_REPARSE = 741,
                /// <summary>
                /// An open/create operation completed while an oplock break is underway.
                /// </summary>
                ERROR_OPLOCK_BREAK_IN_PROGRESS = 742,
                /// <summary>
                /// A new volume has been mounted by a file system.
                /// </summary>
                ERROR_VOLUME_MOUNTED = 743,
                /// <summary>
                /// This success level status indicates that the transaction state already exists for the registry sub-tree, but that a transaction commit was previously aborted. The commit has now been completed.
                /// </summary>
                ERROR_RXACT_COMMITTED = 744,
                /// <summary>
                /// This indicates that a notify change request has been completed due to closing the handle which made the notify change request.
                /// </summary>
                ERROR_NOTIFY_CLEANUP = 745,
                /// <summary>
                /// {Connect Failure on Primary Transport} An attempt was made to connect to the remote server %hs on the primary transport, but the connection failed. The computer WAS able to connect on a secondary transport.
                /// </summary>
                ERROR_PRIMARY_TRANSPORT_CONNECT_FAILED = 746,
                /// <summary>
                /// Page fault was a transition fault.
                /// </summary>
                ERROR_PAGE_FAULT_TRANSITION = 747,
                /// <summary>
                /// Page fault was a demand zero fault.
                /// </summary>
                ERROR_PAGE_FAULT_DEMAND_ZERO = 748,
                /// <summary>
                /// Page fault was a demand zero fault.
                /// </summary>
                ERROR_PAGE_FAULT_COPY_ON_WRITE = 749,
                /// <summary>
                /// Page fault was a demand zero fault.
                /// </summary>
                ERROR_PAGE_FAULT_GUARD_PAGE = 750,
                /// <summary>
                /// Page fault was satisfied by reading from a secondary storage device.
                /// </summary>
                ERROR_PAGE_FAULT_PAGING_FILE = 751,
                /// <summary>
                /// Cached page was locked during operation.
                /// </summary>
                ERROR_CACHE_PAGE_LOCKED = 752,
                /// <summary>
                /// Crash dump exists in paging file.
                /// </summary>
                ERROR_CRASH_DUMP = 753,
                /// <summary>
                /// Specified buffer contains all zeros.
                /// </summary>
                ERROR_BUFFER_ALL_ZEROS = 754,
                /// <summary>
                /// A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.
                /// </summary>
                ERROR_REPARSE_OBJECT = 755,
                /// <summary>
                /// The device has succeeded a query-stop and its resource requirements have changed.
                /// </summary>
                ERROR_RESOURCE_REQUIREMENTS_CHANGED = 756,
                /// <summary>
                /// The translator has translated these resources into the global space and no further translations should be performed.
                /// </summary>
                ERROR_TRANSLATION_COMPLETE = 757,
                /// <summary>
                /// A process being terminated has no threads to terminate.
                /// </summary>
                ERROR_NOTHING_TO_TERMINATE = 758,
                /// <summary>
                /// The specified process is not part of a job.
                /// </summary>
                ERROR_PROCESS_NOT_IN_JOB = 759,
                /// <summary>
                /// The specified process is part of a job.
                /// </summary>
                ERROR_PROCESS_IN_JOB = 760,
                /// <summary>
                /// {Volume Shadow Copy Service} The system is now ready for hibernation.
                /// </summary>
                ERROR_VOLSNAP_HIBERNATE_READY = 761,
                /// <summary>
                /// A file system or file system filter driver has successfully completed an FsFilter operation.
                /// </summary>
                ERROR_FSFILTER_OP_COMPLETED_SUCCESSFULLY = 762,
                /// <summary>
                /// The specified interrupt vector was already connected.
                /// </summary>
                ERROR_INTERRUPT_VECTOR_ALREADY_CONNECTED = 763,
                /// <summary>
                /// The specified interrupt vector is still connected.
                /// </summary>
                ERROR_INTERRUPT_STILL_CONNECTED = 764,
                /// <summary>
                /// An operation is blocked waiting for an oplock.
                /// </summary>
                ERROR_WAIT_FOR_OPLOCK = 765,
                /// <summary>
                /// Debugger handled exception.
                /// </summary>
                ERROR_DBG_EXCEPTION_HANDLED = 766,
                /// <summary>
                /// Debugger continued.
                /// </summary>
                ERROR_DBG_CONTINUE = 767,
                /// <summary>
                /// An exception occurred in a user mode callback and the kernel callback frame should be removed.
                /// </summary>
                ERROR_CALLBACK_POP_STACK = 768,
                /// <summary>
                /// Compression is disabled for this volume.
                /// </summary>
                ERROR_COMPRESSION_DISABLED = 769,
                /// <summary>
                /// The data provider cannot fetch backwards through a result set.
                /// </summary>
                ERROR_CANTFETCHBACKWARDS = 770,
                /// <summary>
                /// The data provider cannot scroll backwards through a result set.
                /// </summary>
                ERROR_CANTSCROLLBACKWARDS = 771,
                /// <summary>
                /// The data provider requires that previously fetched data is released before asking for more data.
                /// </summary>
                ERROR_ROWSNOTRELEASED = 772,
                /// <summary>
                /// The data provider was not able to interpret the flags set for a column binding in an accessor.
                /// </summary>
                ERROR_BAD_ACCESSOR_FLAGS = 773,
                /// <summary>
                /// One or more errors occurred while processing the request.
                /// </summary>
                ERROR_ERRORS_ENCOUNTERED = 774,
                /// <summary>
                /// The implementation is not capable of performing the request.
                /// </summary>
                ERROR_NOT_CAPABLE = 775,
                /// <summary>
                /// The client of a component requested an operation which is not valid given the state of the component instance.
                /// </summary>
                ERROR_REQUEST_OUT_OF_SEQUENCE = 776,
                /// <summary>
                /// A version number could not be parsed.
                /// </summary>
                ERROR_VERSION_PARSE_ERROR = 777,
                /// <summary>
                /// The iterator's start position is invalid.
                /// </summary>
                ERROR_BADSTARTPOSITION = 778,
                /// <summary>
                /// The hardware has reported an uncorrectable memory error.
                /// </summary>
                ERROR_MEMORY_HARDWARE = 779,
                /// <summary>
                /// The attempted operation required self healing to be enabled.
                /// </summary>
                ERROR_DISK_REPAIR_DISABLED = 780,
                /// <summary>
                /// The Desktop heap encountered an error while allocating session memory. There is more information in the system event log.
                /// </summary>
                ERROR_INSUFFICIENT_RESOURCE_FOR_SPECIFIED_SHARED_SECTION_SIZE = 781,
                /// <summary>
                /// The system power state is transitioning from %2 to %3.
                /// </summary>
                ERROR_SYSTEM_POWERSTATE_TRANSITION = 782,
                /// <summary>
                /// The system power state is transitioning from %2 to %3 but could enter %4.
                /// </summary>
                ERROR_SYSTEM_POWERSTATE_COMPLEX_TRANSITION = 783,
                /// <summary>
                /// A thread is getting dispatched with MCA EXCEPTION because of MCA.
                /// </summary>
                ERROR_MCA_EXCEPTION = 784,
                /// <summary>
                /// Access to %1 is monitored by policy rule %2.
                /// </summary>
                ERROR_ACCESS_AUDIT_BY_POLICY = 785,
                /// <summary>
                /// Access to %1 has been restricted by your Administrator by policy rule %2.
                /// </summary>
                ERROR_ACCESS_DISABLED_NO_SAFER_UI_BY_POLICY = 786,
                /// <summary>
                /// A valid hibernation file has been invalidated and should be abandoned.
                /// </summary>
                ERROR_ABANDON_HIBERFILE = 787,
                /// <summary>
                /// {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error may be caused by network connectivity issues. Please try to save this file elsewhere.
                /// </summary>
                ERROR_LOST_WRITEBEHIND_DATA_NETWORK_DISCONNECTED = 788,
                /// <summary>
                /// {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error was returned by the server on which the file exists. Please try to save this file elsewhere.
                /// </summary>
                ERROR_LOST_WRITEBEHIND_DATA_NETWORK_SERVER_ERROR = 789,
                /// <summary>
                /// {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error may be caused if the device has been removed or the media is write-protected.
                /// </summary>
                ERROR_LOST_WRITEBEHIND_DATA_LOCAL_DISK_ERROR = 790,
                /// <summary>
                /// The resources required for this device conflict with the MCFG table.
                /// </summary>
                ERROR_BAD_MCFG_TABLE = 791,
                /// <summary>
                /// The volume repair could not be performed while it is online. Please schedule to take the volume offline so that it can be repaired.
                /// </summary>
                ERROR_DISK_REPAIR_REDIRECTED = 792,
                /// <summary>
                /// The volume repair was not successful.
                /// </summary>
                ERROR_DISK_REPAIR_UNSUCCESSFUL = 793,
                /// <summary>
                /// One of the volume corruption logs is full. Further corruptions that may be detected won't be logged.
                /// </summary>
                ERROR_CORRUPT_LOG_OVERFULL = 794,
                /// <summary>
                /// One of the volume corruption logs is internally corrupted and needs to be recreated. The volume may contain undetected corruptions and must be scanned.
                /// </summary>
                ERROR_CORRUPT_LOG_CORRUPTED = 795,
                /// <summary>
                /// One of the volume corruption logs is unavailable for being operated on.
                /// </summary>
                ERROR_CORRUPT_LOG_UNAVAILABLE = 796,
                /// <summary>
                /// One of the volume corruption logs was deleted while still having corruption records in them. The volume contains detected corruptions and must be scanned.
                /// </summary>
                ERROR_CORRUPT_LOG_DELETED_FULL = 797,
                /// <summary>
                /// One of the volume corruption logs was cleared by chkdsk and no longer contains real corruptions.
                /// </summary>
                ERROR_CORRUPT_LOG_CLEARED = 798,
                /// <summary>
                /// Orphaned files exist on the volume but could not be recovered because no more new names could be created in the recovery directory. Files must be moved from the recovery directory.
                /// </summary>
                ERROR_ORPHAN_NAME_EXHAUSTED = 799,
                /// <summary>
                /// The oplock that was associated with this handle is now associated with a different handle.
                /// </summary>
                ERROR_OPLOCK_SWITCHED_TO_NEW_HANDLE = 800,
                /// <summary>
                /// An oplock of the requested level cannot be granted. An oplock of a lower level may be available.
                /// </summary>
                ERROR_CANNOT_GRANT_REQUESTED_OPLOCK = 801,
                /// <summary>
                /// The operation did not complete successfully because it would cause an oplock to be broken. The caller has requested that existing oplocks not be broken.
                /// </summary>
                ERROR_CANNOT_BREAK_OPLOCK = 802,
                /// <summary>
                /// The handle with which this oplock was associated has been closed. The oplock is now broken.
                /// </summary>
                ERROR_OPLOCK_HANDLE_CLOSED = 803,
                /// <summary>
                /// The specified access control entry (ACE) does not contain a condition.
                /// </summary>
                ERROR_NO_ACE_CONDITION = 804,
                /// <summary>
                /// The specified access control entry (ACE) contains an invalid condition.
                /// </summary>
                ERROR_INVALID_ACE_CONDITION = 805,
                /// <summary>
                /// Access to the specified file handle has been revoked.
                /// </summary>
                ERROR_FILE_HANDLE_REVOKED = 806,
                /// <summary>
                /// An image file was mapped at a different address from the one specified in the image file but fixups will still be automatically performed on the image.
                /// </summary>
                ERROR_IMAGE_AT_DIFFERENT_BASE = 807,
                /// <summary>
                /// Access to the extended attribute was denied.
                /// </summary>
                ERROR_EA_ACCESS_DENIED = 994,
                /// <summary>
                /// The I/O operation has been aborted because of either a thread exit or an application request.
                /// </summary>
                ERROR_OPERATION_ABORTED = 995,
                /// <summary>
                /// Overlapped I/O event is not in a signaled state.
                /// </summary>
                ERROR_IO_INCOMPLETE = 996,
                /// <summary>
                /// Overlapped I/O operation is in progress.
                /// </summary>
                ERROR_IO_PENDING = 997,
                /// <summary>
                /// Invalid access to memory location.
                /// </summary>
                ERROR_NOACCESS = 998,
                /// <summary>
                /// Error performing inpage operation.
                /// </summary>
                ERROR_SWAPERROR = 999,
                /// <summary>
                /// Recursion too deep; the stack overflowed.
                /// </summary>
                ERROR_STACK_OVERFLOW = 1001,
                /// <summary>
                /// The window cannot act on the sent message.
                /// </summary>
                ERROR_INVALID_MESSAGE = 1002,
                /// <summary>
                /// Cannot complete this function.
                /// </summary>
                ERROR_CAN_NOT_COMPLETE = 1003,
                /// <summary>
                /// Invalid flags.
                /// </summary>
                ERROR_INVALID_FLAGS = 1004,
                /// <summary>
                /// The volume does not contain a recognized file system. Please make sure that all required file system drivers are loaded and that the volume is not corrupted.
                /// </summary>
                ERROR_UNRECOGNIZED_VOLUME = 1005,
                /// <summary>
                /// The volume for a file has been externally altered so that the opened file is no longer valid.
                /// </summary>
                ERROR_FILE_INVALID = 1006,
                /// <summary>
                /// The requested operation cannot be performed in full-screen mode.
                /// </summary>
                ERROR_FULLSCREEN_MODE = 1007,
                /// <summary>
                /// An attempt was made to reference a token that does not exist.
                /// </summary>
                ERROR_NO_TOKEN = 1008,
                /// <summary>
                /// The configuration registry database is corrupt.
                /// </summary>
                ERROR_BADDB = 1009,
                /// <summary>
                /// The configuration registry key is invalid.
                /// </summary>
                ERROR_BADKEY = 1010,
                /// <summary>
                /// The configuration registry key could not be opened.
                /// </summary>
                ERROR_CANTOPEN = 1011,
                /// <summary>
                /// The configuration registry key could not be read.
                /// </summary>
                ERROR_CANTREAD = 1012,
                /// <summary>
                /// The configuration registry key could not be written.
                /// </summary>
                ERROR_CANTWRITE = 1013,
                /// <summary>
                /// One of the files in the registry database had to be recovered by use of a log or alternate copy. The recovery was successful.
                /// </summary>
                ERROR_REGISTRY_RECOVERED = 1014,
                /// <summary>
                /// The registry is corrupted. The structure of one of the files containing registry data is corrupted, or the system's memory image of the file is corrupted, or the file could not be recovered because the alternate copy or log was absent or corrupted.
                /// </summary>
                ERROR_REGISTRY_CORRUPT = 1015,
                /// <summary>
                /// An I/O operation initiated by the registry failed unrecoverably. The registry could not read in, or write out, or flush, one of the files that contain the system's image of the registry.
                /// </summary>
                ERROR_REGISTRY_IO_FAILED = 1016,
                /// <summary>
                /// The system has attempted to load or restore a file into the registry, but the specified file is not in a registry file format.
                /// </summary>
                ERROR_NOT_REGISTRY_FILE = 1017,
                /// <summary>
                /// Illegal operation attempted on a registry key that has been marked for deletion.
                /// </summary>
                ERROR_KEY_DELETED = 1018,
                /// <summary>
                /// System could not allocate the required space in a registry log.
                /// </summary>
                ERROR_NO_LOG_SPACE = 1019,
                /// <summary>
                /// Cannot create a symbolic link in a registry key that already has subkeys or values.
                /// </summary>
                ERROR_KEY_HAS_CHILDREN = 1020,
                /// <summary>
                /// Cannot create a stable subkey under a volatile parent key.
                /// </summary>
                ERROR_CHILD_MUST_BE_VOLATILE = 1021,
                /// <summary>
                /// A notify change request is being completed and the information is not being returned in the caller's buffer. The caller now needs to enumerate the files to find the changes.
                /// </summary>
                ERROR_NOTIFY_ENUM_DIR = 1022,
                /// <summary>
                /// A stop control has been sent to a service that other running services are dependent on.
                /// </summary>
                ERROR_DEPENDENT_SERVICES_RUNNING = 1051,
                /// <summary>
                /// The requested control is not valid for this service.
                /// </summary>
                ERROR_INVALID_SERVICE_CONTROL = 1052,
                /// <summary>
                /// The service did not respond to the start or control request in a timely fashion.
                /// </summary>
                ERROR_SERVICE_REQUEST_TIMEOUT = 1053,
                /// <summary>
                /// A thread could not be created for the service.
                /// </summary>
                ERROR_SERVICE_NO_THREAD = 1054,
                /// <summary>
                /// The service database is locked.
                /// </summary>
                ERROR_SERVICE_DATABASE_LOCKED = 1055,
                /// <summary>
                /// An instance of the service is already running.
                /// </summary>
                ERROR_SERVICE_ALREADY_RUNNING = 1056,
                /// <summary>
                /// The account name is invalid or does not exist, or the password is invalid for the account name specified.
                /// </summary>
                ERROR_INVALID_SERVICE_ACCOUNT = 1057,
                /// <summary>
                /// The service cannot be started, either because it is disabled or because it has no enabled devices associated with it.
                /// </summary>
                ERROR_SERVICE_DISABLED = 1058,
                /// <summary>
                /// Circular service dependency was specified.
                /// </summary>
                ERROR_CIRCULAR_DEPENDENCY = 1059,
                /// <summary>
                /// The specified service does not exist as an installed service.
                /// </summary>
                ERROR_SERVICE_DOES_NOT_EXIST = 1060,
                /// <summary>
                /// The service cannot accept control messages at this time.
                /// </summary>
                ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 1061,
                /// <summary>
                /// The service has not been started.
                /// </summary>
                ERROR_SERVICE_NOT_ACTIVE = 1062,
                /// <summary>
                /// The service process could not connect to the service controller.
                /// </summary>
                ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 1063,
                /// <summary>
                /// An exception occurred in the service when handling the control request.
                /// </summary>
                ERROR_EXCEPTION_IN_SERVICE = 1064,
                /// <summary>
                /// The database specified does not exist.
                /// </summary>
                ERROR_DATABASE_DOES_NOT_EXIST = 1065,
                /// <summary>
                /// The service has returned a service-specific error code.
                /// </summary>
                ERROR_SERVICE_SPECIFIC_ERROR = 1066,
                /// <summary>
                /// The process terminated unexpectedly.
                /// </summary>
                ERROR_PROCESS_ABORTED = 1067,
                /// <summary>
                /// The dependency service or group failed to start.
                /// </summary>
                ERROR_SERVICE_DEPENDENCY_FAIL = 1068,
                /// <summary>
                /// The service did not start due to a logon failure.
                /// </summary>
                ERROR_SERVICE_LOGON_FAILED = 1069,
                /// <summary>
                /// After starting, the service hung in a start-pending state.
                /// </summary>
                ERROR_SERVICE_START_HANG = 1070,
                /// <summary>
                /// The specified service database lock is invalid.
                /// </summary>
                ERROR_INVALID_SERVICE_LOCK = 1071,
                /// <summary>
                /// The specified service has been marked for deletion.
                /// </summary>
                ERROR_SERVICE_MARKED_FOR_DELETE = 1072,
                /// <summary>
                /// The specified service already exists.
                /// </summary>
                ERROR_SERVICE_EXISTS = 1073,
                /// <summary>
                /// The system is currently running with the last-known-good configuration.
                /// </summary>
                ERROR_ALREADY_RUNNING_LKG = 1074,
                /// <summary>
                /// The dependency service does not exist or has been marked for deletion.
                /// </summary>
                ERROR_SERVICE_DEPENDENCY_DELETED = 1075,
                /// <summary>
                /// The current boot has already been accepted for use as the last-known-good control set.
                /// </summary>
                ERROR_BOOT_ALREADY_ACCEPTED = 1076,
                /// <summary>
                /// No attempts to start the service have been made since the last boot.
                /// </summary>
                ERROR_SERVICE_NEVER_STARTED = 1077,
                /// <summary>
                /// The name is already in use as either a service name or a service display name.
                /// </summary>
                ERROR_DUPLICATE_SERVICE_NAME = 1078,
                /// <summary>
                /// The account specified for this service is different from the account specified for other services running in the same process.
                /// </summary>
                ERROR_DIFFERENT_SERVICE_ACCOUNT = 1079,
                /// <summary>
                /// Failure actions can only be set for Win32 services, not for drivers.
                /// </summary>
                ERROR_CANNOT_DETECT_DRIVER_FAILURE = 1080,
                /// <summary>
                /// This service runs in the same process as the service control manager. Therefore, the service control manager cannot take action if this service's process terminates unexpectedly.
                /// </summary>
                ERROR_CANNOT_DETECT_PROCESS_ABORT = 1081,
                /// <summary>
                /// No recovery program has been configured for this service.
                /// </summary>
                ERROR_NO_RECOVERY_PROGRAM = 1082,
                /// <summary>
                /// The executable program that this service is configured to run in does not implement the service.
                /// </summary>
                ERROR_SERVICE_NOT_IN_EXE = 1083,
                /// <summary>
                /// This service cannot be started in Safe Mode.
                /// </summary>
                ERROR_NOT_SAFEBOOT_SERVICE = 1084,
                /// <summary>
                /// The physical end of the tape has been reached.
                /// </summary>
                ERROR_END_OF_MEDIA = 1100,
                /// <summary>
                /// A tape access reached a filemark.
                /// </summary>
                ERROR_FILEMARK_DETECTED = 1101,
                /// <summary>
                /// The beginning of the tape or a partition was encountered.
                /// </summary>
                ERROR_BEGINNING_OF_MEDIA = 1102,
                /// <summary>
                /// A tape access reached the end of a set of files.
                /// </summary>
                ERROR_SETMARK_DETECTED = 1103,
                /// <summary>
                /// No more data is on the tape.
                /// </summary>
                ERROR_NO_DATA_DETECTED = 1104,
                /// <summary>
                /// Tape could not be partitioned.
                /// </summary>
                ERROR_PARTITION_FAILURE = 1105,
                /// <summary>
                /// When accessing a new tape of a multivolume partition, the current block size is incorrect.
                /// </summary>
                ERROR_INVALID_BLOCK_LENGTH = 1106,
                /// <summary>
                /// Tape partition information could not be found when loading a tape.
                /// </summary>
                ERROR_DEVICE_NOT_PARTITIONED = 1107,
                /// <summary>
                /// Unable to lock the media eject mechanism.
                /// </summary>
                ERROR_UNABLE_TO_LOCK_MEDIA = 1108,
                /// <summary>
                /// Unable to unload the media.
                /// </summary>
                ERROR_UNABLE_TO_UNLOAD_MEDIA = 1109,
                /// <summary>
                /// The media in the drive may have changed.
                /// </summary>
                ERROR_MEDIA_CHANGED = 1110,
                /// <summary>
                /// The I/O bus was reset.
                /// </summary>
                ERROR_BUS_RESET = 1111,
                /// <summary>
                /// No media in drive.
                /// </summary>
                ERROR_NO_MEDIA_IN_DRIVE = 1112,
                /// <summary>
                /// No mapping for the Unicode character exists in the target multi-byte code page.
                /// </summary>
                ERROR_NO_UNICODE_TRANSLATION = 1113,
                /// <summary>
                /// A dynamic link library (DLL) initialization routine failed.
                /// </summary>
                ERROR_DLL_INIT_FAILED = 1114,
                /// <summary>
                /// A system shutdown is in progress.
                /// </summary>
                ERROR_SHUTDOWN_IN_PROGRESS = 1115,
                /// <summary>
                /// Unable to abort the system shutdown because no shutdown was in progress.
                /// </summary>
                ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116,
                /// <summary>
                /// The request could not be performed because of an I/O device error.
                /// </summary>
                ERROR_IO_DEVICE = 1117,
                /// <summary>
                /// No serial device was successfully initialized. The serial driver will unload.
                /// </summary>
                ERROR_SERIAL_NO_DEVICE = 1118,
                /// <summary>
                /// Unable to open a device that was sharing an interrupt request (IRQ) with other devices. At least one other device that uses that IRQ was already opened.
                /// </summary>
                ERROR_IRQ_BUSY = 1119,
                /// <summary>
                /// A serial I/O operation was completed by another write to the serial port. The IOCTL_SERIAL_XOFF_COUNTER reached zero.)
                /// </summary>
                ERROR_MORE_WRITES = 1120,
                /// <summary>
                /// A serial I/O operation completed because the timeout period expired. The IOCTL_SERIAL_XOFF_COUNTER did not reach zero.)
                /// </summary>
                ERROR_COUNTER_TIMEOUT = 1121,
                /// <summary>
                /// No ID address mark was found on the floppy disk.
                /// </summary>
                ERROR_FLOPPY_ID_MARK_NOT_FOUND = 1122,
                /// <summary>
                /// Mismatch between the floppy disk sector ID field and the floppy disk controller track address.
                /// </summary>
                ERROR_FLOPPY_WRONG_CYLINDER = 1123,
                /// <summary>
                /// The floppy disk controller reported an error that is not recognized by the floppy disk driver.
                /// </summary>
                ERROR_FLOPPY_UNKNOWN_ERROR = 1124,
                /// <summary>
                /// The floppy disk controller returned inconsistent results in its registers.
                /// </summary>
                ERROR_FLOPPY_BAD_REGISTERS = 1125,
                /// <summary>
                /// While accessing the hard disk, a recalibrate operation failed, even after retries.
                /// </summary>
                ERROR_DISK_RECALIBRATE_FAILED = 1126,
                /// <summary>
                /// While accessing the hard disk, a disk operation failed even after retries.
                /// </summary>
                ERROR_DISK_OPERATION_FAILED = 1127,
                /// <summary>
                /// While accessing the hard disk, a disk controller reset was needed, but even that failed.
                /// </summary>
                ERROR_DISK_RESET_FAILED = 1128,
                /// <summary>
                /// Physical end of tape encountered.
                /// </summary>
                ERROR_EOM_OVERFLOW = 1129,
                /// <summary>
                /// Not enough server storage is available to process this command.
                /// </summary>
                ERROR_NOT_ENOUGH_SERVER_MEMORY = 1130,
                /// <summary>
                /// A potential deadlock condition has been detected.
                /// </summary>
                ERROR_POSSIBLE_DEADLOCK = 1131,
                /// <summary>
                /// The base address or the file offset specified does not have the proper alignment.
                /// </summary>
                ERROR_MAPPED_ALIGNMENT = 1132,
                /// <summary>
                /// An attempt to change the system power state was vetoed by another application or driver.
                /// </summary>
                ERROR_SET_POWER_STATE_VETOED = 1140,
                /// <summary>
                /// The system BIOS failed an attempt to change the system power state.
                /// </summary>
                ERROR_SET_POWER_STATE_FAILED = 1141,
                /// <summary>
                /// An attempt was made to create more links on a file than the file system supports.
                /// </summary>
                ERROR_TOO_MANY_LINKS = 1142,
                /// <summary>
                /// The specified program requires a newer version of Windows.
                /// </summary>
                ERROR_OLD_WIN_VERSION = 1150,
                /// <summary>
                /// The specified program is not a Windows or MS-DOS program.
                /// </summary>
                ERROR_APP_WRONG_OS = 1151,
                /// <summary>
                /// Cannot start more than one instance of the specified program.
                /// </summary>
                ERROR_SINGLE_INSTANCE_APP = 1152,
                /// <summary>
                /// The specified program was written for an earlier version of Windows.
                /// </summary>
                ERROR_RMODE_APP = 1153,
                /// <summary>
                /// One of the library files needed to run this application is damaged.
                /// </summary>
                ERROR_INVALID_DLL = 1154,
                /// <summary>
                /// No application is associated with the specified file for this operation.
                /// </summary>
                ERROR_NO_ASSOCIATION = 1155,
                /// <summary>
                /// An error occurred in sending the command to the application.
                /// </summary>
                ERROR_DDE_FAIL = 1156,
                /// <summary>
                /// One of the library files needed to run this application cannot be found.
                /// </summary>
                ERROR_DLL_NOT_FOUND = 1157,
                /// <summary>
                /// The current process has used all of its system allowance of handles for Window Manager objects.
                /// </summary>
                ERROR_NO_MORE_USER_HANDLES = 1158,
                /// <summary>
                /// The message can be used only with synchronous operations.
                /// </summary>
                ERROR_MESSAGE_SYNC_ONLY = 1159,
                /// <summary>
                /// The indicated source element has no media.
                /// </summary>
                ERROR_SOURCE_ELEMENT_EMPTY = 1160,
                /// <summary>
                /// The indicated destination element already contains media.
                /// </summary>
                ERROR_DESTINATION_ELEMENT_FULL = 1161,
                /// <summary>
                /// The indicated element does not exist.
                /// </summary>
                ERROR_ILLEGAL_ELEMENT_ADDRESS = 1162,
                /// <summary>
                /// The indicated element is part of a magazine that is not present.
                /// </summary>
                ERROR_MAGAZINE_NOT_PRESENT = 1163,
                /// <summary>
                /// The indicated device requires reinitialization due to hardware errors.
                /// </summary>
                ERROR_DEVICE_REINITIALIZATION_NEEDED = 1164,
                /// <summary>
                /// The device has indicated that cleaning is required before further operations are attempted.
                /// </summary>
                ERROR_DEVICE_REQUIRES_CLEANING = 1165,
                /// <summary>
                /// The device has indicated that its door is open.
                /// </summary>
                ERROR_DEVICE_DOOR_OPEN = 1166,
                /// <summary>
                /// The device is not connected.
                /// </summary>
                ERROR_DEVICE_NOT_CONNECTED = 1167,
                /// <summary>
                /// Element not found.
                /// </summary>
                ERROR_NOT_FOUND = 1168,
                /// <summary>
                /// There was no match for the specified key in the index.
                /// </summary>
                ERROR_NO_MATCH = 1169,
                /// <summary>
                /// The property set specified does not exist on the object.
                /// </summary>
                ERROR_SET_NOT_FOUND = 1170,
                /// <summary>
                /// The point passed to GetMouseMovePoints is not in the buffer.
                /// </summary>
                ERROR_POINT_NOT_FOUND = 1171,
                /// <summary>
                /// The tracking (workstation) service is not running.
                /// </summary>
                ERROR_NO_TRACKING_SERVICE = 1172,
                /// <summary>
                /// The Volume ID could not be found.
                /// </summary>
                ERROR_NO_VOLUME_ID = 1173,
                /// <summary>
                /// Unable to remove the file to be replaced.
                /// </summary>
                ERROR_UNABLE_TO_REMOVE_REPLACED = 1175,
                /// <summary>
                /// Unable to move the replacement file to the file to be replaced. The file to be replaced has retained its original name.
                /// </summary>
                ERROR_UNABLE_TO_MOVE_REPLACEMENT = 1176,
                /// <summary>
                /// Unable to move the replacement file to the file to be replaced. The file to be replaced has been renamed using the backup name.
                /// </summary>
                ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 = 1177,
                /// <summary>
                /// The volume change journal is being deleted.
                /// </summary>
                ERROR_JOURNAL_DELETE_IN_PROGRESS = 1178,
                /// <summary>
                /// The volume change journal is not active.
                /// </summary>
                ERROR_JOURNAL_NOT_ACTIVE = 1179,
                /// <summary>
                /// A file was found, but it may not be the correct file.
                /// </summary>
                ERROR_POTENTIAL_FILE_FOUND = 1180,
                /// <summary>
                /// The journal entry has been deleted from the journal.
                /// </summary>
                ERROR_JOURNAL_ENTRY_DELETED = 1181,
                /// <summary>
                /// A system shutdown has already been scheduled.
                /// </summary>
                ERROR_SHUTDOWN_IS_SCHEDULED = 1190,
                /// <summary>
                /// The system shutdown cannot be initiated because there are other users logged on to the computer.
                /// </summary>
                ERROR_SHUTDOWN_USERS_LOGGED_ON = 1191,
                /// <summary>
                /// The specified device name is invalid.
                /// </summary>
                ERROR_BAD_DEVICE = 1200,
                /// <summary>
                /// The device is not currently connected but it is a remembered connection.
                /// </summary>
                ERROR_CONNECTION_UNAVAIL = 1201,
                /// <summary>
                /// The local device name has a remembered connection to another network resource.
                /// </summary>
                ERROR_DEVICE_ALREADY_REMEMBERED = 1202,
                /// <summary>
                /// The network path was either typed incorrectly, does not exist, or the network provider is not currently available. Please try retyping the path or contact your network administrator.
                /// </summary>
                ERROR_NO_NET_OR_BAD_PATH = 1203,
                /// <summary>
                /// The specified network provider name is invalid.
                /// </summary>
                ERROR_BAD_PROVIDER = 1204,
                /// <summary>
                /// Unable to open the network connection profile.
                /// </summary>
                ERROR_CANNOT_OPEN_PROFILE = 1205,
                /// <summary>
                /// The network connection profile is corrupted.
                /// </summary>
                ERROR_BAD_PROFILE = 1206,
                /// <summary>
                /// Cannot enumerate a noncontainer.
                /// </summary>
                ERROR_NOT_CONTAINER = 1207,
                /// <summary>
                /// An extended error has occurred.
                /// </summary>
                ERROR_EXTENDED_ERROR = 1208,
                /// <summary>
                /// The format of the specified group name is invalid.
                /// </summary>
                ERROR_INVALID_GROUPNAME = 1209,
                /// <summary>
                /// The format of the specified computer name is invalid.
                /// </summary>
                ERROR_INVALID_COMPUTERNAME = 1210,
                /// <summary>
                /// The format of the specified event name is invalid.
                /// </summary>
                ERROR_INVALID_EVENTNAME = 1211,
                /// <summary>
                /// The format of the specified domain name is invalid.
                /// </summary>
                ERROR_INVALID_DOMAINNAME = 1212,
                /// <summary>
                /// The format of the specified service name is invalid.
                /// </summary>
                ERROR_INVALID_SERVICENAME = 1213,
                /// <summary>
                /// The format of the specified network name is invalid.
                /// </summary>
                ERROR_INVALID_NETNAME = 1214,
                /// <summary>
                /// The format of the specified share name is invalid.
                /// </summary>
                ERROR_INVALID_SHARENAME = 1215,
                /// <summary>
                /// The format of the specified password is invalid.
                /// </summary>
                ERROR_INVALID_PASSWORDNAME = 1216,
                /// <summary>
                /// The format of the specified message name is invalid.
                /// </summary>
                ERROR_INVALID_MESSAGENAME = 1217,
                /// <summary>
                /// The format of the specified message destination is invalid.
                /// </summary>
                ERROR_INVALID_MESSAGEDEST = 1218,
                /// <summary>
                /// Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again.
                /// </summary>
                ERROR_SESSION_CREDENTIAL_CONFLICT = 1219,
                /// <summary>
                /// An attempt was made to establish a session to a network server, but there are already too many sessions established to that server.
                /// </summary>
                ERROR_REMOTE_SESSION_LIMIT_EXCEEDED = 1220,
                /// <summary>
                /// The workgroup or domain name is already in use by another computer on the network.
                /// </summary>
                ERROR_DUP_DOMAINNAME = 1221,
                /// <summary>
                /// The network is not present or not started.
                /// </summary>
                ERROR_NO_NETWORK = 1222,
                /// <summary>
                /// The operation was canceled by the user.
                /// </summary>
                ERROR_CANCELLED = 1223,
                /// <summary>
                /// The requested operation cannot be performed on a file with a user-mapped section open.
                /// </summary>
                ERROR_USER_MAPPED_FILE = 1224,
                /// <summary>
                /// The remote computer refused the network connection.
                /// </summary>
                ERROR_CONNECTION_REFUSED = 1225,
                /// <summary>
                /// The network connection was gracefully closed.
                /// </summary>
                ERROR_GRACEFUL_DISCONNECT = 1226,
                /// <summary>
                /// The network transport endpoint already has an address associated with it.
                /// </summary>
                ERROR_ADDRESS_ALREADY_ASSOCIATED = 1227,
                /// <summary>
                /// An address has not yet been associated with the network endpoint.
                /// </summary>
                ERROR_ADDRESS_NOT_ASSOCIATED = 1228,
                /// <summary>
                /// An operation was attempted on a nonexistent network connection.
                /// </summary>
                ERROR_CONNECTION_INVALID = 1229,
                /// <summary>
                /// An invalid operation was attempted on an active network connection.
                /// </summary>
                ERROR_CONNECTION_ACTIVE = 1230,
                /// <summary>
                /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
                /// </summary>
                ERROR_NETWORK_UNREACHABLE = 1231,
                /// <summary>
                /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
                /// </summary>
                ERROR_HOST_UNREACHABLE = 1232,
                /// <summary>
                /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
                /// </summary>
                ERROR_PROTOCOL_UNREACHABLE = 1233,
                /// <summary>
                /// No service is operating at the destination network endpoint on the remote system.
                /// </summary>
                ERROR_PORT_UNREACHABLE = 1234,
                /// <summary>
                /// The request was aborted.
                /// </summary>
                ERROR_REQUEST_ABORTED = 1235,
                /// <summary>
                /// The network connection was aborted by the local system.
                /// </summary>
                ERROR_CONNECTION_ABORTED = 1236,
                /// <summary>
                /// The operation could not be completed. A retry should be performed.
                /// </summary>
                ERROR_RETRY = 1237,
                /// <summary>
                /// A connection to the server could not be made because the limit on the number of concurrent connections for this account has been reached.
                /// </summary>
                ERROR_CONNECTION_COUNT_LIMIT = 1238,
                /// <summary>
                /// Attempting to log in during an unauthorized time of day for this account.
                /// </summary>
                ERROR_LOGIN_TIME_RESTRICTION = 1239,
                /// <summary>
                /// The account is not authorized to log in from this station.
                /// </summary>
                ERROR_LOGIN_WKSTA_RESTRICTION = 1240,
                /// <summary>
                /// The network address could not be used for the operation requested.
                /// </summary>
                ERROR_INCORRECT_ADDRESS = 1241,
                /// <summary>
                /// The service is already registered.
                /// </summary>
                ERROR_ALREADY_REGISTERED = 1242,
                /// <summary>
                /// The specified service does not exist.
                /// </summary>
                ERROR_SERVICE_NOT_FOUND = 1243,
                /// <summary>
                /// The operation being requested was not performed because the user has not been authenticated.
                /// </summary>
                ERROR_NOT_AUTHENTICATED = 1244,
                /// <summary>
                /// The operation being requested was not performed because the user has not logged on to the network. The specified service does not exist.
                /// </summary>
                ERROR_NOT_LOGGED_ON = 1245,
                /// <summary>
                /// Continue with work in progress.
                /// </summary>
                ERROR_CONTINUE = 1246,
                /// <summary>
                /// An attempt was made to perform an initialization operation when initialization has already been completed.
                /// </summary>
                ERROR_ALREADY_INITIALIZED = 1247,
                /// <summary>
                /// No more local devices.
                /// </summary>
                ERROR_NO_MORE_DEVICES = 1248,
                /// <summary>
                /// The specified site does not exist.
                /// </summary>
                ERROR_NO_SUCH_SITE = 1249,
                /// <summary>
                /// A domain controller with the specified name already exists.
                /// </summary>
                ERROR_DOMAIN_CONTROLLER_EXISTS = 1250,
                /// <summary>
                /// This operation is supported only when you are connected to the server.
                /// </summary>
                ERROR_ONLY_IF_CONNECTED = 1251,
                /// <summary>
                /// The group policy framework should call the extension even if there are no changes.
                /// </summary>
                ERROR_OVERRIDE_NOCHANGES = 1252,
                /// <summary>
                /// The specified user does not have a valid profile.
                /// </summary>
                ERROR_BAD_USER_PROFILE = 1253,
                /// <summary>
                /// This operation is not supported on a computer running Windows Server 2003 for Small Business Server.
                /// </summary>
                ERROR_NOT_SUPPORTED_ON_SBS = 1254,
                /// <summary>
                /// The server machine is shutting down.
                /// </summary>
                ERROR_SERVER_SHUTDOWN_IN_PROGRESS = 1255,
                /// <summary>
                /// The remote system is not available. For information about network troubleshooting, see Windows Help.
                /// </summary>
                ERROR_HOST_DOWN = 1256,
                /// <summary>
                /// The security identifier provided is not from an account domain.
                /// </summary>
                ERROR_NON_ACCOUNT_SID = 1257,
                /// <summary>
                /// The security identifier provided does not have a domain component.
                /// </summary>
                ERROR_NON_DOMAIN_SID = 1258,
                /// <summary>
                /// AppHelp dialog canceled thus preventing the application from starting.
                /// </summary>
                ERROR_APPHELP_BLOCK = 1259,
                /// <summary>
                /// This program is blocked by group policy. For more information, contact your system administrator.
                /// </summary>
                ERROR_ACCESS_DISABLED_BY_POLICY = 1260,
                /// <summary>
                /// A program attempt to use an invalid register value. Normally caused by an uninitialized register. This error is Itanium specific.
                /// </summary>
                ERROR_REG_NAT_CONSUMPTION = 1261,
                /// <summary>
                /// The share is currently offline or does not exist.
                /// </summary>
                ERROR_CSCSHARE_OFFLINE = 1262,
                /// <summary>
                /// The Kerberos protocol encountered an error while validating the KDC certificate during smartcard logon. There is more information in the system event log.
                /// </summary>
                ERROR_PKINIT_FAILURE = 1263,
                /// <summary>
                /// The Kerberos protocol encountered an error while attempting to utilize the smartcard subsystem.
                /// </summary>
                ERROR_SMARTCARD_SUBSYSTEM_FAILURE = 1264,
                /// <summary>
                /// The system cannot contact a domain controller to service the authentication request. Please try again later.
                /// </summary>
                ERROR_DOWNGRADE_DETECTED = 1265,
                /// <summary>
                /// The machine is locked and cannot be shut down without the force option.
                /// </summary>
                ERROR_MACHINE_LOCKED = 1271,
                /// <summary>
                /// An application-defined callback gave invalid data when called.
                /// </summary>
                ERROR_CALLBACK_SUPPLIED_INVALID_DATA = 1273,
                /// <summary>
                /// The group policy framework should call the extension in the synchronous foreground policy refresh.
                /// </summary>
                ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED = 1274,
                /// <summary>
                /// This driver has been blocked from loading.
                /// </summary>
                ERROR_DRIVER_BLOCKED = 1275,
                /// <summary>
                /// A dynamic link library (DLL) referenced a module that was neither a DLL nor the process's executable image.
                /// </summary>
                ERROR_INVALID_IMPORT_OF_NON_DLL = 1276,
                /// <summary>
                /// Windows cannot open this program since it has been disabled.
                /// </summary>
                ERROR_ACCESS_DISABLED_WEBBLADE = 1277,
                /// <summary>
                /// Windows cannot open this program because the license enforcement system has been tampered with or become corrupted.
                /// </summary>
                ERROR_ACCESS_DISABLED_WEBBLADE_TAMPER = 1278,
                /// <summary>
                /// A transaction recover failed.
                /// </summary>
                ERROR_RECOVERY_FAILURE = 1279,
                /// <summary>
                /// The current thread has already been converted to a fiber.
                /// </summary>
                ERROR_ALREADY_FIBER = 1280,
                /// <summary>
                /// The current thread has already been converted from a fiber.
                /// </summary>
                ERROR_ALREADY_THREAD = 1281,
                /// <summary>
                /// The system detected an overrun of a stack-based buffer in this application. This overrun could potentially allow a malicious user to gain control of this application.
                /// </summary>
                ERROR_STACK_BUFFER_OVERRUN = 1282,
                /// <summary>
                /// Data present in one of the parameters is more than the function can operate on.
                /// </summary>
                ERROR_PARAMETER_QUOTA_EXCEEDED = 1283,
                /// <summary>
                /// An attempt to do an operation on a debug object failed because the object is in the process of being deleted.
                /// </summary>
                ERROR_DEBUGGER_INACTIVE = 1284,
                /// <summary>
                /// An attempt to delay-load a .dll or get a function address in a delay-loaded .dll failed.
                /// </summary>
                ERROR_DELAY_LOAD_FAILED = 1285,
                /// <summary>
                /// %1 is a 16-bit application. You do not have permissions to execute 16-bit applications. Check your permissions with your system administrator.
                /// </summary>
                ERROR_VDM_DISALLOWED = 1286,
                /// <summary>
                /// Insufficient information exists to identify the cause of failure.
                /// </summary>
                ERROR_UNIDENTIFIED_ERROR = 1287,
                /// <summary>
                /// The parameter passed to a C runtime function is incorrect.
                /// </summary>
                ERROR_INVALID_CRUNTIME_PARAMETER = 1288,
                /// <summary>
                /// The operation occurred beyond the valid data length of the file.
                /// </summary>
                ERROR_BEYOND_VDL = 1289,
                /// <summary>
                /// The service start failed since one or more services in the same process have an incompatible service SID type  setting. A service with restricted service SID type can only coexist in the same process with other services  with a restricted SID type. If the service SID type for this service was just configured, the hosting process  must be restarted in order to start this service. On Windows Server 2003 and Windows XP, an unrestricted service cannot coexist in the  same process with other services. The service with the unrestricted service SID type must be moved to an owned  process in order to start this service.
                /// </summary>
                ERROR_INCOMPATIBLE_SERVICE_SID_TYPE = 1290,
                /// <summary>
                /// The process hosting the driver for this device has been terminated.
                /// </summary>
                ERROR_DRIVER_PROCESS_TERMINATED = 1291,
                /// <summary>
                /// An operation attempted to exceed an implementation-defined limit.
                /// </summary>
                ERROR_IMPLEMENTATION_LIMIT = 1292,
                /// <summary>
                /// Either the target process, or the target thread's containing process, is a protected process.
                /// </summary>
                ERROR_PROCESS_IS_PROTECTED = 1293,
                /// <summary>
                /// The service notification client is lagging too far behind the current state of services in the machine.
                /// </summary>
                ERROR_SERVICE_NOTIFY_CLIENT_LAGGING = 1294,
                /// <summary>
                /// The requested file operation failed because the storage quota was exceeded. To free up disk space, move files to a different location or delete unnecessary files. For more information, contact your system administrator.
                /// </summary>
                ERROR_DISK_QUOTA_EXCEEDED = 1295,
                /// <summary>
                /// The requested file operation failed because the storage policy blocks that type of file. For more information, contact your system administrator.
                /// </summary>
                ERROR_CONTENT_BLOCKED = 1296,
                /// <summary>
                /// A privilege that the service requires to function properly does not exist in the service account configuration. You may use the Services Microsoft Management Console (MMC) snap-in (services.msc) and the Local Security Settings MMC snap-in (secpol.msc) to view the service configuration and the account configuration.
                /// </summary>
                ERROR_INCOMPATIBLE_SERVICE_PRIVILEGE = 1297,
                /// <summary>
                /// A thread involved in this operation appears to be unresponsive.
                /// </summary>
                ERROR_APP_HANG = 1298,
                /// <summary>
                /// Indicates a particular Security ID may not be assigned as the label of an object.
                /// </summary>
                ERROR_INVALID_LABEL = 1299,
                /// <summary>
                /// Not all privileges or groups referenced are assigned to the caller.
                /// </summary>
                ERROR_NOT_ALL_ASSIGNED = 1300,
                /// <summary>
                /// Some mapping between account names and security IDs was not done.
                /// </summary>
                ERROR_SOME_NOT_MAPPED = 1301,
                /// <summary>
                /// No system quota limits are specifically set for this account.
                /// </summary>
                ERROR_NO_QUOTAS_FOR_ACCOUNT = 1302,
                /// <summary>
                /// No encryption key is available. A well-known encryption key was returned.
                /// </summary>
                ERROR_LOCAL_USER_SESSION_KEY = 1303,
                /// <summary>
                /// The password is too complex to be converted to a LAN Manager password. The LAN Manager password returned is a NULL string.
                /// </summary>
                ERROR_NULL_LM_PASSWORD = 1304,
                /// <summary>
                /// The revision level is unknown.
                /// </summary>
                ERROR_UNKNOWN_REVISION = 1305,
                /// <summary>
                /// Indicates two revision levels are incompatible.
                /// </summary>
                ERROR_REVISION_MISMATCH = 1306,
                /// <summary>
                /// This security ID may not be assigned as the owner of this object.
                /// </summary>
                ERROR_INVALID_OWNER = 1307,
                /// <summary>
                /// This security ID may not be assigned as the primary group of an object.
                /// </summary>
                ERROR_INVALID_PRIMARY_GROUP = 1308,
                /// <summary>
                /// An attempt has been made to operate on an impersonation token by a thread that is not currently impersonating a client.
                /// </summary>
                ERROR_NO_IMPERSONATION_TOKEN = 1309,
                /// <summary>
                /// The group may not be disabled.
                /// </summary>
                ERROR_CANT_DISABLE_MANDATORY = 1310,
                /// <summary>
                /// There are currently no logon servers available to service the logon request.
                /// </summary>
                ERROR_NO_LOGON_SERVERS = 1311,
                /// <summary>
                /// A specified logon session does not exist. It may already have been terminated.
                /// </summary>
                ERROR_NO_SUCH_LOGON_SESSION = 1312,
                /// <summary>
                /// A specified privilege does not exist.
                /// </summary>
                ERROR_NO_SUCH_PRIVILEGE = 1313,
                /// <summary>
                /// A required privilege is not held by the client.
                /// </summary>
                ERROR_PRIVILEGE_NOT_HELD = 1314,
                /// <summary>
                /// The name provided is not a properly formed account name.
                /// </summary>
                ERROR_INVALID_ACCOUNT_NAME = 1315,
                /// <summary>
                /// The specified account already exists.
                /// </summary>
                ERROR_USER_EXISTS = 1316,
                /// <summary>
                /// The specified account does not exist.
                /// </summary>
                ERROR_NO_SUCH_USER = 1317,
                /// <summary>
                /// The specified group already exists.
                /// </summary>
                ERROR_GROUP_EXISTS = 1318,
                /// <summary>
                /// The specified group does not exist.
                /// </summary>
                ERROR_NO_SUCH_GROUP = 1319,
                /// <summary>
                /// Either the specified user account is already a member of the specified group, or the specified group cannot be deleted because it contains a member.
                /// </summary>
                ERROR_MEMBER_IN_GROUP = 1320,
                /// <summary>
                /// The specified user account is not a member of the specified group account.
                /// </summary>
                ERROR_MEMBER_NOT_IN_GROUP = 1321,
                /// <summary>
                /// This operation is disallowed as it could result in an administration account being disabled, deleted or unable to log on.
                /// </summary>
                ERROR_LAST_ADMIN = 1322,
                /// <summary>
                /// Unable to update the password. The value provided as the current password is incorrect.
                /// </summary>
                ERROR_WRONG_PASSWORD = 1323,
                /// <summary>
                /// Unable to update the password. The value provided for the new password contains values that are not allowed in passwords.
                /// </summary>
                ERROR_ILL_FORMED_PASSWORD = 1324,
                /// <summary>
                /// Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirements of the domain.
                /// </summary>
                ERROR_PASSWORD_RESTRICTION = 1325,
                /// <summary>
                /// The user name or password is incorrect.
                /// </summary>
                ERROR_LOGON_FAILURE = 1326,
                /// <summary>
                /// Account restrictions are preventing this user from signing in. For example: blank passwords aren't allowed, sign-in times are limited, or a policy restriction has been enforced.
                /// </summary>
                ERROR_ACCOUNT_RESTRICTION = 1327,
                /// <summary>
                /// Your account has time restrictions that keep you from signing in right now.
                /// </summary>
                ERROR_INVALID_LOGON_HOURS = 1328,
                /// <summary>
                /// This user isn't allowed to sign in to this computer.
                /// </summary>
                ERROR_INVALID_WORKSTATION = 1329,
                /// <summary>
                /// The password for this account has expired.
                /// </summary>
                ERROR_PASSWORD_EXPIRED = 1330,
                /// <summary>
                /// This user can't sign in because this account is currently disabled.
                /// </summary>
                ERROR_ACCOUNT_DISABLED = 1331,
                /// <summary>
                /// No mapping between account names and security IDs was done.
                /// </summary>
                ERROR_NONE_MAPPED = 1332,
                /// <summary>
                /// Too many local user identifiers (LUIDs) were requested at one time.
                /// </summary>
                ERROR_TOO_MANY_LUIDS_REQUESTED = 1333,
                /// <summary>
                /// No more local user identifiers (LUIDs) are available.
                /// </summary>
                ERROR_LUIDS_EXHAUSTED = 1334,
                /// <summary>
                /// The subauthority part of a security ID is invalid for this particular use.
                /// </summary>
                ERROR_INVALID_SUB_AUTHORITY = 1335,
                /// <summary>
                /// The access control list (ACL) structure is invalid.
                /// </summary>
                ERROR_INVALID_ACL = 1336,
                /// <summary>
                /// The security ID structure is invalid.
                /// </summary>
                ERROR_INVALID_SID = 1337,
                /// <summary>
                /// The security descriptor structure is invalid.
                /// </summary>
                ERROR_INVALID_SECURITY_DESCR = 1338,
                /// <summary>
                /// The inherited access control list (ACL) or access control entry (ACE) could not be built.
                /// </summary>
                ERROR_BAD_INHERITANCE_ACL = 1340,
                /// <summary>
                /// The server is currently disabled.
                /// </summary>
                ERROR_SERVER_DISABLED = 1341,
                /// <summary>
                /// The server is currently enabled.
                /// </summary>
                ERROR_SERVER_NOT_DISABLED = 1342,
                /// <summary>
                /// The value provided was an invalid value for an identifier authority.
                /// </summary>
                ERROR_INVALID_ID_AUTHORITY = 1343,
                /// <summary>
                /// No more memory is available for security information updates.
                /// </summary>
                ERROR_ALLOTTED_SPACE_EXCEEDED = 1344,
                /// <summary>
                /// The specified attributes are invalid, or incompatible with the attributes for the group as a whole.
                /// </summary>
                ERROR_INVALID_GROUP_ATTRIBUTES = 1345,
                /// <summary>
                /// Either a required impersonation level was not provided, or the provided impersonation level is invalid.
                /// </summary>
                ERROR_BAD_IMPERSONATION_LEVEL = 1346,
                /// <summary>
                /// Cannot open an anonymous level security token.
                /// </summary>
                ERROR_CANT_OPEN_ANONYMOUS = 1347,
                /// <summary>
                /// The validation information class requested was invalid.
                /// </summary>
                ERROR_BAD_VALIDATION_CLASS = 1348,
                /// <summary>
                /// The type of the token is inappropriate for its attempted use.
                /// </summary>
                ERROR_BAD_TOKEN_TYPE = 1349,
                /// <summary>
                /// Unable to perform a security operation on an object that has no associated security.
                /// </summary>
                ERROR_NO_SECURITY_ON_OBJECT = 1350,
                /// <summary>
                /// Configuration information could not be read from the domain controller, either because the machine is unavailable, or access has been denied.
                /// </summary>
                ERROR_CANT_ACCESS_DOMAIN_INFO = 1351,
                /// <summary>
                /// The security account manager (SAM) or local security authority (LSA) server was in the wrong state to perform the security operation.
                /// </summary>
                ERROR_INVALID_SERVER_STATE = 1352,
                /// <summary>
                /// The domain was in the wrong state to perform the security operation.
                /// </summary>
                ERROR_INVALID_DOMAIN_STATE = 1353,
                /// <summary>
                /// This operation is only allowed for the Primary Domain Controller of the domain.
                /// </summary>
                ERROR_INVALID_DOMAIN_ROLE = 1354,
                /// <summary>
                /// The specified domain either does not exist or could not be contacted.
                /// </summary>
                ERROR_NO_SUCH_DOMAIN = 1355,
                /// <summary>
                /// The specified domain already exists.
                /// </summary>
                ERROR_DOMAIN_EXISTS = 1356,
                /// <summary>
                /// An attempt was made to exceed the limit on the number of domains per server.
                /// </summary>
                ERROR_DOMAIN_LIMIT_EXCEEDED = 1357,
                /// <summary>
                /// Unable to complete the requested operation because of either a catastrophic media failure or a data structure corruption on the disk.
                /// </summary>
                ERROR_INTERNAL_DB_CORRUPTION = 1358,
                /// <summary>
                /// An internal error occurred.
                /// </summary>
                ERROR_INTERNAL_ERROR = 1359,
                /// <summary>
                /// Generic access types were contained in an access mask which should already be mapped to nongeneric types.
                /// </summary>
                ERROR_GENERIC_NOT_MAPPED = 1360,
                /// <summary>
                /// A security descriptor is not in the right format (absolute or self-relative).
                /// </summary>
                ERROR_BAD_DESCRIPTOR_FORMAT = 1361,
                /// <summary>
                /// The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process.
                /// </summary>
                ERROR_NOT_LOGON_PROCESS = 1362,
                /// <summary>
                /// Cannot start a new logon session with an ID that is already in use.
                /// </summary>
                ERROR_LOGON_SESSION_EXISTS = 1363,
                /// <summary>
                /// A specified authentication package is unknown.
                /// </summary>
                ERROR_NO_SUCH_PACKAGE = 1364,
                /// <summary>
                /// The logon session is not in a state that is consistent with the requested operation.
                /// </summary>
                ERROR_BAD_LOGON_SESSION_STATE = 1365,
                /// <summary>
                /// The logon session ID is already in use.
                /// </summary>
                ERROR_LOGON_SESSION_COLLISION = 1366,
                /// <summary>
                /// A logon request contained an invalid logon type value.
                /// </summary>
                ERROR_INVALID_LOGON_TYPE = 1367,
                /// <summary>
                /// Unable to impersonate using a named pipe until data has been read from that pipe.
                /// </summary>
                ERROR_CANNOT_IMPERSONATE = 1368,
                /// <summary>
                /// The transaction state of a registry subtree is incompatible with the requested operation.
                /// </summary>
                ERROR_RXACT_INVALID_STATE = 1369,
                /// <summary>
                /// An internal security database corruption has been encountered.
                /// </summary>
                ERROR_RXACT_COMMIT_FAILURE = 1370,
                /// <summary>
                /// Cannot perform this operation on built-in accounts.
                /// </summary>
                ERROR_SPECIAL_ACCOUNT = 1371,
                /// <summary>
                /// Cannot perform this operation on this built-in special group.
                /// </summary>
                ERROR_SPECIAL_GROUP = 1372,
                /// <summary>
                /// Cannot perform this operation on this built-in special user.
                /// </summary>
                ERROR_SPECIAL_USER = 1373,
                /// <summary>
                /// The user cannot be removed from a group because the group is currently the user's primary group.
                /// </summary>
                ERROR_MEMBERS_PRIMARY_GROUP = 1374,
                /// <summary>
                /// The token is already in use as a primary token.
                /// </summary>
                ERROR_TOKEN_ALREADY_IN_USE = 1375,
                /// <summary>
                /// The specified local group does not exist.
                /// </summary>
                ERROR_NO_SUCH_ALIAS = 1376,
                /// <summary>
                /// The specified account name is not a member of the group.
                /// </summary>
                ERROR_MEMBER_NOT_IN_ALIAS = 1377,
                /// <summary>
                /// The specified account name is already a member of the group.
                /// </summary>
                ERROR_MEMBER_IN_ALIAS = 1378,
                /// <summary>
                /// The specified local group already exists.
                /// </summary>
                ERROR_ALIAS_EXISTS = 1379,
                /// <summary>
                /// Logon failure: the user has not been granted the requested logon type at this computer.
                /// </summary>
                ERROR_LOGON_NOT_GRANTED = 1380,
                /// <summary>
                /// The maximum number of secrets that may be stored in a single system has been exceeded.
                /// </summary>
                ERROR_TOO_MANY_SECRETS = 1381,
                /// <summary>
                /// The length of a secret exceeds the maximum length allowed.
                /// </summary>
                ERROR_SECRET_TOO_LONG = 1382,
                /// <summary>
                /// The local security authority database contains an internal inconsistency.
                /// </summary>
                ERROR_INTERNAL_DB_ERROR = 1383,
                /// <summary>
                /// During a logon attempt, the user's security context accumulated too many security IDs.
                /// </summary>
                ERROR_TOO_MANY_CONTEXT_IDS = 1384,
                /// <summary>
                /// Logon failure: the user has not been granted the requested logon type at this computer.
                /// </summary>
                ERROR_LOGON_TYPE_NOT_GRANTED = 1385,
                /// <summary>
                /// A cross-encrypted password is necessary to change a user password.
                /// </summary>
                ERROR_NT_CROSS_ENCRYPTION_REQUIRED = 1386,
                /// <summary>
                /// A member could not be added to or removed from the local group because the member does not exist.
                /// </summary>
                ERROR_NO_SUCH_MEMBER = 1387,
                /// <summary>
                /// A new member could not be added to a local group because the member has the wrong account type.
                /// </summary>
                ERROR_INVALID_MEMBER = 1388,
                /// <summary>
                /// Too many security IDs have been specified.
                /// </summary>
                ERROR_TOO_MANY_SIDS = 1389,
                /// <summary>
                /// A cross-encrypted password is necessary to change this user password.
                /// </summary>
                ERROR_LM_CROSS_ENCRYPTION_REQUIRED = 1390,
                /// <summary>
                /// Indicates an ACL contains no inheritable components.
                /// </summary>
                ERROR_NO_INHERITANCE = 1391,
                /// <summary>
                /// The file or directory is corrupted and unreadable.
                /// </summary>
                ERROR_FILE_CORRUPT = 1392,
                /// <summary>
                /// The disk structure is corrupted and unreadable.
                /// </summary>
                ERROR_DISK_CORRUPT = 1393,
                /// <summary>
                /// There is no user session key for the specified logon session.
                /// </summary>
                ERROR_NO_USER_SESSION_KEY = 1394,
                /// <summary>
                /// The service being accessed is licensed for a particular number of connections. No more connections can be made to the service at this time because there are already as many connections as the service can accept.
                /// </summary>
                ERROR_LICENSE_QUOTA_EXCEEDED = 1395,
                /// <summary>
                /// The target account name is incorrect.
                /// </summary>
                ERROR_WRONG_TARGET_NAME = 1396,
                /// <summary>
                /// Mutual Authentication failed. The server's password is out of date at the domain controller.
                /// </summary>
                ERROR_MUTUAL_AUTH_FAILED = 1397,
                /// <summary>
                /// There is a time and/or date difference between the client and server.
                /// </summary>
                ERROR_TIME_SKEW = 1398,
                /// <summary>
                /// This operation cannot be performed on the current domain.
                /// </summary>
                ERROR_CURRENT_DOMAIN_NOT_ALLOWED = 1399,
                /// <summary>
                /// Invalid window handle.
                /// </summary>
                ERROR_INVALID_WINDOW_HANDLE = 1400,
                /// <summary>
                /// Invalid menu handle.
                /// </summary>
                ERROR_INVALID_MENU_HANDLE = 1401,
                /// <summary>
                /// Invalid cursor handle.
                /// </summary>
                ERROR_INVALID_CURSOR_HANDLE = 1402,
                /// <summary>
                /// Invalid accelerator table handle.
                /// </summary>
                ERROR_INVALID_ACCEL_HANDLE = 1403,
                /// <summary>
                /// Invalid hook handle.
                /// </summary>
                ERROR_INVALID_HOOK_HANDLE = 1404,
                /// <summary>
                /// Invalid handle to a multiple-window position structure.
                /// </summary>
                ERROR_INVALID_DWP_HANDLE = 1405,
                /// <summary>
                /// Cannot create a top-level child window.
                /// </summary>
                ERROR_TLW_WITH_WSCHILD = 1406,
                /// <summary>
                /// Cannot find window class.
                /// </summary>
                ERROR_CANNOT_FIND_WND_CLASS = 1407,
                /// <summary>
                /// Invalid window; it belongs to other thread.
                /// </summary>
                ERROR_WINDOW_OF_OTHER_THREAD = 1408,
                /// <summary>
                /// Hot key is already registered.
                /// </summary>
                ERROR_HOTKEY_ALREADY_REGISTERED = 1409,
                /// <summary>
                /// Class already exists.
                /// </summary>
                ERROR_CLASS_ALREADY_EXISTS = 1410,
                /// <summary>
                /// Class does not exist.
                /// </summary>
                ERROR_CLASS_DOES_NOT_EXIST = 1411,
                /// <summary>
                /// Class still has open windows.
                /// </summary>
                ERROR_CLASS_HAS_WINDOWS = 1412,
                /// <summary>
                /// Invalid index.
                /// </summary>
                ERROR_INVALID_INDEX = 1413,
                /// <summary>
                /// Invalid icon handle.
                /// </summary>
                ERROR_INVALID_ICON_HANDLE = 1414,
                /// <summary>
                /// Using private DIALOG window words.
                /// </summary>
                ERROR_PRIVATE_DIALOG_INDEX = 1415,
                /// <summary>
                /// The list box identifier was not found.
                /// </summary>
                ERROR_LISTBOX_ID_NOT_FOUND = 1416,
                /// <summary>
                /// No wildcards were found.
                /// </summary>
                ERROR_NO_WILDCARD_CHARACTERS = 1417,
                /// <summary>
                /// Thread does not have a clipboard open.
                /// </summary>
                ERROR_CLIPBOARD_NOT_OPEN = 1418,
                /// <summary>
                /// Hot key is not registered.
                /// </summary>
                ERROR_HOTKEY_NOT_REGISTERED = 1419,
                /// <summary>
                /// The window is not a valid dialog window.
                /// </summary>
                ERROR_WINDOW_NOT_DIALOG = 1420,
                /// <summary>
                /// Control ID not found.
                /// </summary>
                ERROR_CONTROL_ID_NOT_FOUND = 1421,
                /// <summary>
                /// Invalid message for a combo box because it does not have an edit control.
                /// </summary>
                ERROR_INVALID_COMBOBOX_MESSAGE = 1422,
                /// <summary>
                /// The window is not a combo box.
                /// </summary>
                ERROR_WINDOW_NOT_COMBOBOX = 1423,
                /// <summary>
                /// Height must be less than 256.
                /// </summary>
                ERROR_INVALID_EDIT_HEIGHT = 1424,
                /// <summary>
                /// Invalid device context (DC) handle.
                /// </summary>
                ERROR_DC_NOT_FOUND = 1425,
                /// <summary>
                /// Invalid hook procedure type.
                /// </summary>
                ERROR_INVALID_HOOK_FILTER = 1426,
                /// <summary>
                /// Invalid hook procedure.
                /// </summary>
                ERROR_INVALID_FILTER_PROC = 1427,
                /// <summary>
                /// Cannot set nonlocal hook without a module handle.
                /// </summary>
                ERROR_HOOK_NEEDS_HMOD = 1428,
                /// <summary>
                /// This hook procedure can only be set globally.
                /// </summary>
                ERROR_GLOBAL_ONLY_HOOK = 1429,
                /// <summary>
                /// The journal hook procedure is already installed.
                /// </summary>
                ERROR_JOURNAL_HOOK_SET = 1430,
                /// <summary>
                /// The hook procedure is not installed.
                /// </summary>
                ERROR_HOOK_NOT_INSTALLED = 1431,
                /// <summary>
                /// Invalid message for single-selection list box.
                /// </summary>
                ERROR_INVALID_LB_MESSAGE = 1432,
                /// <summary>
                /// LB_SETCOUNT sent to non-lazy list box.
                /// </summary>
                ERROR_SETCOUNT_ON_BAD_LB = 1433,
                /// <summary>
                /// This list box does not support tab stops.
                /// </summary>
                ERROR_LB_WITHOUT_TABSTOPS = 1434,
                /// <summary>
                /// Cannot destroy object created by another thread.
                /// </summary>
                ERROR_DESTROY_OBJECT_OF_OTHER_THREAD = 1435,
                /// <summary>
                /// Child windows cannot have menus.
                /// </summary>
                ERROR_CHILD_WINDOW_MENU = 1436,
                /// <summary>
                /// The window does not have a system menu.
                /// </summary>
                ERROR_NO_SYSTEM_MENU = 1437,
                /// <summary>
                /// Invalid message box style.
                /// </summary>
                ERROR_INVALID_MSGBOX_STYLE = 1438,
                /// <summary>
                /// Invalid system-wide (SPI_*) parameter.
                /// </summary>
                ERROR_INVALID_SPI_VALUE = 1439,
                /// <summary>
                /// Screen already locked.
                /// </summary>
                ERROR_SCREEN_ALREADY_LOCKED = 1440,
                /// <summary>
                /// All handles to windows in a multiple-window position structure must have the same parent.
                /// </summary>
                ERROR_HWNDS_HAVE_DIFF_PARENT = 1441,
                /// <summary>
                /// The window is not a child window.
                /// </summary>
                ERROR_NOT_CHILD_WINDOW = 1442,
                /// <summary>
                /// Invalid GW_* command.
                /// </summary>
                ERROR_INVALID_GW_COMMAND = 1443,
                /// <summary>
                /// Invalid thread identifier.
                /// </summary>
                ERROR_INVALID_THREAD_ID = 1444,
                /// <summary>
                /// Cannot process a message from a window that is not a multiple document interface (MDI) window.
                /// </summary>
                ERROR_NON_MDICHILD_WINDOW = 1445,
                /// <summary>
                /// Popup menu already active.
                /// </summary>
                ERROR_POPUP_ALREADY_ACTIVE = 1446,
                /// <summary>
                /// The window does not have scroll bars.
                /// </summary>
                ERROR_NO_SCROLLBARS = 1447,
                /// <summary>
                /// Scroll bar range cannot be greater than MAXLONG.
                /// </summary>
                ERROR_INVALID_SCROLLBAR_RANGE = 1448,
                /// <summary>
                /// Cannot show or remove the window in the way specified.
                /// </summary>
                ERROR_INVALID_SHOWWIN_COMMAND = 1449,
                /// <summary>
                /// Insufficient system resources exist to complete the requested service.
                /// </summary>
                ERROR_NO_SYSTEM_RESOURCES = 1450,
                /// <summary>
                /// Insufficient system resources exist to complete the requested service.
                /// </summary>
                ERROR_NONPAGED_SYSTEM_RESOURCES = 1451,
                /// <summary>
                /// Insufficient system resources exist to complete the requested service.
                /// </summary>
                ERROR_PAGED_SYSTEM_RESOURCES = 1452,
                /// <summary>
                /// Insufficient quota to complete the requested service.
                /// </summary>
                ERROR_WORKING_SET_QUOTA = 1453,
                /// <summary>
                /// Insufficient quota to complete the requested service.
                /// </summary>
                ERROR_PAGEFILE_QUOTA = 1454,
                /// <summary>
                /// The paging file is too small for this operation to complete.
                /// </summary>
                ERROR_COMMITMENT_LIMIT = 1455,
                /// <summary>
                /// A menu item was not found.
                /// </summary>
                ERROR_MENU_ITEM_NOT_FOUND = 1456,
                /// <summary>
                /// Invalid keyboard layout handle.
                /// </summary>
                ERROR_INVALID_KEYBOARD_HANDLE = 1457,
                /// <summary>
                /// Hook type not allowed.
                /// </summary>
                ERROR_HOOK_TYPE_NOT_ALLOWED = 1458,
                /// <summary>
                /// This operation requires an interactive window station.
                /// </summary>
                ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION = 1459,
                /// <summary>
                /// This operation returned because the timeout period expired.
                /// </summary>
                ERROR_TIMEOUT = 1460,
                /// <summary>
                /// Invalid monitor handle.
                /// </summary>
                ERROR_INVALID_MONITOR_HANDLE = 1461,
                /// <summary>
                /// Incorrect size argument.
                /// </summary>
                ERROR_INCORRECT_SIZE = 1462,
                /// <summary>
                /// The symbolic link cannot be followed because its type is disabled.
                /// </summary>
                ERROR_SYMLINK_CLASS_DISABLED = 1463,
                /// <summary>
                /// This application does not support the current operation on symbolic links.
                /// </summary>
                ERROR_SYMLINK_NOT_SUPPORTED = 1464,
                /// <summary>
                /// Windows was unable to parse the requested XML data.
                /// </summary>
                ERROR_XML_PARSE_ERROR = 1465,
                /// <summary>
                /// An error was encountered while processing an XML digital signature.
                /// </summary>
                ERROR_XMLDSIG_ERROR = 1466,
                /// <summary>
                /// This application must be restarted.
                /// </summary>
                ERROR_RESTART_APPLICATION = 1467,
                /// <summary>
                /// The caller made the connection request in the wrong routing compartment.
                /// </summary>
                ERROR_WRONG_COMPARTMENT = 1468,
                /// <summary>
                /// There was an AuthIP failure when attempting to connect to the remote host.
                /// </summary>
                ERROR_AUTHIP_FAILURE = 1469,
                /// <summary>
                /// Insufficient NVRAM resources exist to complete the requested service. A reboot might be required.
                /// </summary>
                ERROR_NO_NVRAM_RESOURCES = 1470,
                /// <summary>
                /// Unable to finish the requested operation because the specified process is not a GUI process.
                /// </summary>
                ERROR_NOT_GUI_PROCESS = 1471,
                /// <summary>
                /// The event log file is corrupted.
                /// </summary>
                ERROR_EVENTLOG_FILE_CORRUPT = 1500,
                /// <summary>
                /// No event log file could be opened, so the event logging service did not start.
                /// </summary>
                ERROR_EVENTLOG_CANT_START = 1501,
                /// <summary>
                /// The event log file is full.
                /// </summary>
                ERROR_LOG_FILE_FULL = 1502,
                /// <summary>
                /// The event log file has changed between read operations.
                /// </summary>
                ERROR_EVENTLOG_FILE_CHANGED = 1503,
                /// <summary>
                /// The specified task name is invalid.
                /// </summary>
                ERROR_INVALID_TASK_NAME = 1550,
                /// <summary>
                /// The specified task index is invalid.
                /// </summary>
                ERROR_INVALID_TASK_INDEX = 1551,
                /// <summary>
                /// The specified thread is already joining a task.
                /// </summary>
                ERROR_THREAD_ALREADY_IN_TASK = 1552,
                /// <summary>
                /// The Windows Installer Service could not be accessed. This can occur if the Windows Installer is not correctly installed. Contact your support personnel for assistance.
                /// </summary>
                ERROR_INSTALL_SERVICE_FAILURE = 1601,
                /// <summary>
                /// User cancelled installation.
                /// </summary>
                ERROR_INSTALL_USEREXIT = 1602,
                /// <summary>
                /// Fatal error during installation.
                /// </summary>
                ERROR_INSTALL_FAILURE = 1603,
                /// <summary>
                /// Installation suspended, incomplete.
                /// </summary>
                ERROR_INSTALL_SUSPEND = 1604,
                /// <summary>
                /// This action is only valid for products that are currently installed.
                /// </summary>
                ERROR_UNKNOWN_PRODUCT = 1605,
                /// <summary>
                /// Feature ID not registered.
                /// </summary>
                ERROR_UNKNOWN_FEATURE = 1606,
                /// <summary>
                /// Component ID not registered.
                /// </summary>
                ERROR_UNKNOWN_COMPONENT = 1607,
                /// <summary>
                /// Unknown property.
                /// </summary>
                ERROR_UNKNOWN_PROPERTY = 1608,
                /// <summary>
                /// Handle is in an invalid state.
                /// </summary>
                ERROR_INVALID_HANDLE_STATE = 1609,
                /// <summary>
                /// The configuration data for this product is corrupt. Contact your support personnel.
                /// </summary>
                ERROR_BAD_CONFIGURATION = 1610,
                /// <summary>
                /// Component qualifier not present.
                /// </summary>
                ERROR_INDEX_ABSENT = 1611,
                /// <summary>
                /// The installation source for this product is not available. Verify that the source exists and that you can access it.
                /// </summary>
                ERROR_INSTALL_SOURCE_ABSENT = 1612,
                /// <summary>
                /// This installation package cannot be installed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.
                /// </summary>
                ERROR_INSTALL_PACKAGE_VERSION = 1613,
                /// <summary>
                /// Product is uninstalled.
                /// </summary>
                ERROR_PRODUCT_UNINSTALLED = 1614,
                /// <summary>
                /// SQL query syntax invalid or unsupported.
                /// </summary>
                ERROR_BAD_QUERY_SYNTAX = 1615,
                /// <summary>
                /// Record field does not exist.
                /// </summary>
                ERROR_INVALID_FIELD = 1616,
                /// <summary>
                /// The device has been removed.
                /// </summary>
                ERROR_DEVICE_REMOVED = 1617,
                /// <summary>
                /// Another installation is already in progress. Complete that installation before proceeding with this install.
                /// </summary>
                ERROR_INSTALL_ALREADY_RUNNING = 1618,
                /// <summary>
                /// This installation package could not be opened. Verify that the package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer package.
                /// </summary>
                ERROR_INSTALL_PACKAGE_OPEN_FAILED = 1619,
                /// <summary>
                /// This installation package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer package.
                /// </summary>
                ERROR_INSTALL_PACKAGE_INVALID = 1620,
                /// <summary>
                /// There was an error starting the Windows Installer service user interface. Contact your support personnel.
                /// </summary>
                ERROR_INSTALL_UI_FAILURE = 1621,
                /// <summary>
                /// Error opening installation log file. Verify that the specified log file location exists and that you can write to it.
                /// </summary>
                ERROR_INSTALL_LOG_FAILURE = 1622,
                /// <summary>
                /// The language of this installation package is not supported by your system.
                /// </summary>
                ERROR_INSTALL_LANGUAGE_UNSUPPORTED = 1623,
                /// <summary>
                /// Error applying transforms. Verify that the specified transform paths are valid.
                /// </summary>
                ERROR_INSTALL_TRANSFORM_FAILURE = 1624,
                /// <summary>
                /// This installation is forbidden by system policy. Contact your system administrator.
                /// </summary>
                ERROR_INSTALL_PACKAGE_REJECTED = 1625,
                /// <summary>
                /// Function could not be executed.
                /// </summary>
                ERROR_FUNCTION_NOT_CALLED = 1626,
                /// <summary>
                /// Function failed during execution.
                /// </summary>
                ERROR_FUNCTION_FAILED = 1627,
                /// <summary>
                /// Invalid or unknown table specified.
                /// </summary>
                ERROR_INVALID_TABLE = 1628,
                /// <summary>
                /// Data supplied is of wrong type.
                /// </summary>
                ERROR_DATATYPE_MISMATCH = 1629,
                /// <summary>
                /// Data of this type is not supported.
                /// </summary>
                ERROR_UNSUPPORTED_TYPE = 1630,
                /// <summary>
                /// The Windows Installer service failed to start. Contact your support personnel.
                /// </summary>
                ERROR_CREATE_FAILED = 1631,
                /// <summary>
                /// The Temp folder is on a drive that is full or is inaccessible. Free up space on the drive or verify that you have write permission on the Temp folder.
                /// </summary>
                ERROR_INSTALL_TEMP_UNWRITABLE = 1632,
                /// <summary>
                /// This installation package is not supported by this processor type. Contact your product vendor.
                /// </summary>
                ERROR_INSTALL_PLATFORM_UNSUPPORTED = 1633,
                /// <summary>
                /// Component not used on this computer.
                /// </summary>
                ERROR_INSTALL_NOTUSED = 1634,
                /// <summary>
                /// This update package could not be opened. Verify that the update package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer update package.
                /// </summary>
                ERROR_PATCH_PACKAGE_OPEN_FAILED = 1635,
                /// <summary>
                /// This update package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer update package.
                /// </summary>
                ERROR_PATCH_PACKAGE_INVALID = 1636,
                /// <summary>
                /// This update package cannot be processed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.
                /// </summary>
                ERROR_PATCH_PACKAGE_UNSUPPORTED = 1637,
                /// <summary>
                /// Another version of this product is already installed. Installation of this version cannot continue. To configure or remove the existing version of this product, use Add/Remove Programs on the Control Panel.
                /// </summary>
                ERROR_PRODUCT_VERSION = 1638,
                /// <summary>
                /// Invalid command line argument. Consult the Windows Installer SDK for detailed command line help.
                /// </summary>
                ERROR_INVALID_COMMAND_LINE = 1639,
                /// <summary>
                /// Only administrators have permission to add, remove, or configure server software during a Terminal services remote session. If you want to install or configure software on the server, contact your network administrator.
                /// </summary>
                ERROR_INSTALL_REMOTE_DISALLOWED = 1640,
                /// <summary>
                /// The requested operation completed successfully. The system will be restarted so the changes can take effect.
                /// </summary>
                ERROR_SUCCESS_REBOOT_INITIATED = 1641,
                /// <summary>
                /// The upgrade cannot be installed by the Windows Installer service because the program to be upgraded may be missing, or the upgrade may update a different version of the program. Verify that the program to be upgraded exists on your computer and that you have the correct upgrade.
                /// </summary>
                ERROR_PATCH_TARGET_NOT_FOUND = 1642,
                /// <summary>
                /// The update package is not permitted by software restriction policy.
                /// </summary>
                ERROR_PATCH_PACKAGE_REJECTED = 1643,
                /// <summary>
                /// One or more customizations are not permitted by software restriction policy.
                /// </summary>
                ERROR_INSTALL_TRANSFORM_REJECTED = 1644,
                /// <summary>
                /// The Windows Installer does not permit installation from a Remote Desktop Connection.
                /// </summary>
                ERROR_INSTALL_REMOTE_PROHIBITED = 1645,
                /// <summary>
                /// Uninstallation of the update package is not supported.
                /// </summary>
                ERROR_PATCH_REMOVAL_UNSUPPORTED = 1646,
                /// <summary>
                /// The update is not applied to this product.
                /// </summary>
                ERROR_UNKNOWN_PATCH = 1647,
                /// <summary>
                /// No valid sequence could be found for the set of updates.
                /// </summary>
                ERROR_PATCH_NO_SEQUENCE = 1648,
                /// <summary>
                /// Update removal was disallowed by policy.
                /// </summary>
                ERROR_PATCH_REMOVAL_DISALLOWED = 1649,
                /// <summary>
                /// The XML update data is invalid.
                /// </summary>
                ERROR_INVALID_PATCH_XML = 1650,
                /// <summary>
                /// Windows Installer does not permit updating of managed advertised products. At least one feature of the product must be installed before applying the update.
                /// </summary>
                ERROR_PATCH_MANAGED_ADVERTISED_PRODUCT = 1651,
                /// <summary>
                /// The Windows Installer service is not accessible in Safe Mode. Please try again when your computer is not in Safe Mode or you can use System Restore to return your machine to a previous good state.
                /// </summary>
                ERROR_INSTALL_SERVICE_SAFEBOOT = 1652,
                /// <summary>
                /// A fail fast exception occurred. Exception handlers will not be invoked and the process will be terminated immediately.
                /// </summary>
                ERROR_FAIL_FAST_EXCEPTION = 1653,
                /// <summary>
                /// The app that you are trying to run is not supported on this version of Windows.
                /// </summary>
                ERROR_INSTALL_REJECTED = 1654,
                /// <summary>
                /// The string binding is invalid.
                /// </summary>
                RPC_S_INVALID_STRING_BINDING = 1700,
                /// <summary>
                /// The binding handle is not the correct type.
                /// </summary>
                RPC_S_WRONG_KIND_OF_BINDING = 1701,
                /// <summary>
                /// The binding handle is invalid.
                /// </summary>
                RPC_S_INVALID_BINDING = 1702,
                /// <summary>
                /// The RPC protocol sequence is not supported.
                /// </summary>
                RPC_S_PROTSEQ_NOT_SUPPORTED = 1703,
                /// <summary>
                /// The RPC protocol sequence is invalid.
                /// </summary>
                RPC_S_INVALID_RPC_PROTSEQ = 1704,
                /// <summary>
                /// The string universal unique identifier (UUID) is invalid.
                /// </summary>
                RPC_S_INVALID_STRING_UUID = 1705,
                /// <summary>
                /// The endpoint format is invalid.
                /// </summary>
                RPC_S_INVALID_ENDPOINT_FORMAT = 1706,
                /// <summary>
                /// The network address is invalid.
                /// </summary>
                RPC_S_INVALID_NET_ADDR = 1707,
                /// <summary>
                /// No endpoint was found.
                /// </summary>
                RPC_S_NO_ENDPOINT_FOUND = 1708,
                /// <summary>
                /// The timeout value is invalid.
                /// </summary>
                RPC_S_INVALID_TIMEOUT = 1709,
                /// <summary>
                /// The object universal unique identifier (UUID) was not found.
                /// </summary>
                RPC_S_OBJECT_NOT_FOUND = 1710,
                /// <summary>
                /// The object universal unique identifier (UUID) has already been registered.
                /// </summary>
                RPC_S_ALREADY_REGISTERED = 1711,
                /// <summary>
                /// The type universal unique identifier (UUID) has already been registered.
                /// </summary>
                RPC_S_TYPE_ALREADY_REGISTERED = 1712,
                /// <summary>
                /// The RPC server is already listening.
                /// </summary>
                RPC_S_ALREADY_LISTENING = 1713,
                /// <summary>
                /// No protocol sequences have been registered.
                /// </summary>
                RPC_S_NO_PROTSEQS_REGISTERED = 1714,
                /// <summary>
                /// The RPC server is not listening.
                /// </summary>
                RPC_S_NOT_LISTENING = 1715,
                /// <summary>
                /// The manager type is unknown.
                /// </summary>
                RPC_S_UNKNOWN_MGR_TYPE = 1716,
                /// <summary>
                /// The interface is unknown.
                /// </summary>
                RPC_S_UNKNOWN_IF = 1717,
                /// <summary>
                /// There are no bindings.
                /// </summary>
                RPC_S_NO_BINDINGS = 1718,
                /// <summary>
                /// There are no protocol sequences.
                /// </summary>
                RPC_S_NO_PROTSEQS = 1719,
                /// <summary>
                /// The endpoint cannot be created.
                /// </summary>
                RPC_S_CANT_CREATE_ENDPOINT = 1720,
                /// <summary>
                /// Not enough resources are available to complete this operation.
                /// </summary>
                RPC_S_OUT_OF_RESOURCES = 1721,
                /// <summary>
                /// The RPC server is unavailable.
                /// </summary>
                RPC_S_SERVER_UNAVAILABLE = 1722,
                /// <summary>
                /// The RPC server is too busy to complete this operation.
                /// </summary>
                RPC_S_SERVER_TOO_BUSY = 1723,
                /// <summary>
                /// The network options are invalid.
                /// </summary>
                RPC_S_INVALID_NETWORK_OPTIONS = 1724,
                /// <summary>
                /// There are no remote procedure calls active on this thread.
                /// </summary>
                RPC_S_NO_CALL_ACTIVE = 1725,
                /// <summary>
                /// The remote procedure call failed.
                /// </summary>
                RPC_S_CALL_FAILED = 1726,
                /// <summary>
                /// The remote procedure call failed and did not execute.
                /// </summary>
                RPC_S_CALL_FAILED_DNE = 1727,
                /// <summary>
                /// A remote procedure call (RPC) protocol error occurred.
                /// </summary>
                RPC_S_PROTOCOL_ERROR = 1728,
                /// <summary>
                /// Access to the HTTP proxy is denied.
                /// </summary>
                RPC_S_PROXY_ACCESS_DENIED = 1729,
                /// <summary>
                /// The transfer syntax is not supported by the RPC server.
                /// </summary>
                RPC_S_UNSUPPORTED_TRANS_SYN = 1730,
                /// <summary>
                /// The universal unique identifier (UUID) type is not supported.
                /// </summary>
                RPC_S_UNSUPPORTED_TYPE = 1732,
                /// <summary>
                /// The tag is invalid.
                /// </summary>
                RPC_S_INVALID_TAG = 1733,
                /// <summary>
                /// The array bounds are invalid.
                /// </summary>
                RPC_S_INVALID_BOUND = 1734,
                /// <summary>
                /// The binding does not contain an entry name.
                /// </summary>
                RPC_S_NO_ENTRY_NAME = 1735,
                /// <summary>
                /// The name syntax is invalid.
                /// </summary>
                RPC_S_INVALID_NAME_SYNTAX = 1736,
                /// <summary>
                /// The name syntax is not supported.
                /// </summary>
                RPC_S_UNSUPPORTED_NAME_SYNTAX = 1737,
                /// <summary>
                /// No network address is available to use to construct a universal unique identifier (UUID).
                /// </summary>
                RPC_S_UUID_NO_ADDRESS = 1739,
                /// <summary>
                /// The endpoint is a duplicate.
                /// </summary>
                RPC_S_DUPLICATE_ENDPOINT = 1740,
                /// <summary>
                /// The authentication type is unknown.
                /// </summary>
                RPC_S_UNKNOWN_AUTHN_TYPE = 1741,
                /// <summary>
                /// The maximum number of calls is too small.
                /// </summary>
                RPC_S_MAX_CALLS_TOO_SMALL = 1742,
                /// <summary>
                /// The string is too long.
                /// </summary>
                RPC_S_STRING_TOO_LONG = 1743,
                /// <summary>
                /// The RPC protocol sequence was not found.
                /// </summary>
                RPC_S_PROTSEQ_NOT_FOUND = 1744,
                /// <summary>
                /// The procedure number is out of range.
                /// </summary>
                RPC_S_PROCNUM_OUT_OF_RANGE = 1745,
                /// <summary>
                /// The binding does not contain any authentication information.
                /// </summary>
                RPC_S_BINDING_HAS_NO_AUTH = 1746,
                /// <summary>
                /// The authentication service is unknown.
                /// </summary>
                RPC_S_UNKNOWN_AUTHN_SERVICE = 1747,
                /// <summary>
                /// The authentication level is unknown.
                /// </summary>
                RPC_S_UNKNOWN_AUTHN_LEVEL = 1748,
                /// <summary>
                /// The security context is invalid.
                /// </summary>
                RPC_S_INVALID_AUTH_IDENTITY = 1749,
                /// <summary>
                /// The authorization service is unknown.
                /// </summary>
                RPC_S_UNKNOWN_AUTHZ_SERVICE = 1750,
                /// <summary>
                /// The entry is invalid.
                /// </summary>
                EPT_S_INVALID_ENTRY = 1751,
                /// <summary>
                /// The server endpoint cannot perform the operation.
                /// </summary>
                EPT_S_CANT_PERFORM_OP = 1752,
                /// <summary>
                /// There are no more endpoints available from the endpoint mapper.
                /// </summary>
                EPT_S_NOT_REGISTERED = 1753,
                /// <summary>
                /// No interfaces have been exported.
                /// </summary>
                RPC_S_NOTHING_TO_EXPORT = 1754,
                /// <summary>
                /// The entry name is incomplete.
                /// </summary>
                RPC_S_INCOMPLETE_NAME = 1755,
                /// <summary>
                /// The version option is invalid.
                /// </summary>
                RPC_S_INVALID_VERS_OPTION = 1756,
                /// <summary>
                /// There are no more members.
                /// </summary>
                RPC_S_NO_MORE_MEMBERS = 1757,
                /// <summary>
                /// There is nothing to unexport.
                /// </summary>
                RPC_S_NOT_ALL_OBJS_UNEXPORTED = 1758,
                /// <summary>
                /// The interface was not found.
                /// </summary>
                RPC_S_INTERFACE_NOT_FOUND = 1759,
                /// <summary>
                /// The entry already exists.
                /// </summary>
                RPC_S_ENTRY_ALREADY_EXISTS = 1760,
                /// <summary>
                /// The entry is not found.
                /// </summary>
                RPC_S_ENTRY_NOT_FOUND = 1761,
                /// <summary>
                /// The name service is unavailable.
                /// </summary>
                RPC_S_NAME_SERVICE_UNAVAILABLE = 1762,
                /// <summary>
                /// The network address family is invalid.
                /// </summary>
                RPC_S_INVALID_NAF_ID = 1763,
                /// <summary>
                /// The requested operation is not supported.
                /// </summary>
                RPC_S_CANNOT_SUPPORT = 1764,
                /// <summary>
                /// No security context is available to allow impersonation.
                /// </summary>
                RPC_S_NO_CONTEXT_AVAILABLE = 1765,
                /// <summary>
                /// An internal error occurred in a remote procedure call (RPC).
                /// </summary>
                RPC_S_INTERNAL_ERROR = 1766,
                /// <summary>
                /// The RPC server attempted an integer division by zero.
                /// </summary>
                RPC_S_ZERO_DIVIDE = 1767,
                /// <summary>
                /// An addressing error occurred in the RPC server.
                /// </summary>
                RPC_S_ADDRESS_ERROR = 1768,
                /// <summary>
                /// A floating-point operation at the RPC server caused a division by zero.
                /// </summary>
                RPC_S_FP_DIV_ZERO = 1769,
                /// <summary>
                /// A floating-point underflow occurred at the RPC server.
                /// </summary>
                RPC_S_FP_UNDERFLOW = 1770,
                /// <summary>
                /// A floating-point overflow occurred at the RPC server.
                /// </summary>
                RPC_S_FP_OVERFLOW = 1771,
                /// <summary>
                /// The list of RPC servers available for the binding of auto handles has been exhausted.
                /// </summary>
                RPC_X_NO_MORE_ENTRIES = 1772,
                /// <summary>
                /// Unable to open the character translation table file.
                /// </summary>
                RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 1773,
                /// <summary>
                /// The file containing the character translation table has fewer than 512 bytes.
                /// </summary>
                RPC_X_SS_CHAR_TRANS_SHORT_FILE = 1774,
                /// <summary>
                /// A null context handle was passed from the client to the host during a remote procedure call.
                /// </summary>
                RPC_X_SS_IN_NULL_CONTEXT = 1775,
                /// <summary>
                /// The context handle changed during a remote procedure call.
                /// </summary>
                RPC_X_SS_CONTEXT_DAMAGED = 1777,
                /// <summary>
                /// The binding handles passed to a remote procedure call do not match.
                /// </summary>
                RPC_X_SS_HANDLES_MISMATCH = 1778,
                /// <summary>
                /// The stub is unable to get the remote procedure call handle.
                /// </summary>
                RPC_X_SS_CANNOT_GET_CALL_HANDLE = 1779,
                /// <summary>
                /// A null reference pointer was passed to the stub.
                /// </summary>
                RPC_X_NULL_REF_POINTER = 1780,
                /// <summary>
                /// The enumeration value is out of range.
                /// </summary>
                RPC_X_ENUM_VALUE_OUT_OF_RANGE = 1781,
                /// <summary>
                /// The byte count is too small.
                /// </summary>
                RPC_X_BYTE_COUNT_TOO_SMALL = 1782,
                /// <summary>
                /// The stub received bad data.
                /// </summary>
                RPC_X_BAD_STUB_DATA = 1783,
                /// <summary>
                /// The supplied user buffer is not valid for the requested operation.
                /// </summary>
                ERROR_INVALID_USER_BUFFER = 1784,
                /// <summary>
                /// The disk media is not recognized. It may not be formatted.
                /// </summary>
                ERROR_UNRECOGNIZED_MEDIA = 1785,
                /// <summary>
                /// The workstation does not have a trust secret.
                /// </summary>
                ERROR_NO_TRUST_LSA_SECRET = 1786,
                /// <summary>
                /// The security database on the server does not have a computer account for this workstation trust relationship.
                /// </summary>
                ERROR_NO_TRUST_SAM_ACCOUNT = 1787,
                /// <summary>
                /// The trust relationship between the primary domain and the trusted domain failed.
                /// </summary>
                ERROR_TRUSTED_DOMAIN_FAILURE = 1788,
                /// <summary>
                /// The trust relationship between this workstation and the primary domain failed.
                /// </summary>
                ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789,
                /// <summary>
                /// The network logon failed.
                /// </summary>
                ERROR_TRUST_FAILURE = 1790,
                /// <summary>
                /// A remote procedure call is already in progress for this thread.
                /// </summary>
                RPC_S_CALL_IN_PROGRESS = 1791,
                /// <summary>
                /// An attempt was made to logon, but the network logon service was not started.
                /// </summary>
                ERROR_NETLOGON_NOT_STARTED = 1792,
                /// <summary>
                /// The user's account has expired.
                /// </summary>
                ERROR_ACCOUNT_EXPIRED = 1793,
                /// <summary>
                /// The redirector is in use and cannot be unloaded.
                /// </summary>
                ERROR_REDIRECTOR_HAS_OPEN_HANDLES = 1794,
                /// <summary>
                /// The specified printer driver is already installed.
                /// </summary>
                ERROR_PRINTER_DRIVER_ALREADY_INSTALLED = 1795,
                /// <summary>
                /// The specified port is unknown.
                /// </summary>
                ERROR_UNKNOWN_PORT = 1796,
                /// <summary>
                /// The printer driver is unknown.
                /// </summary>
                ERROR_UNKNOWN_PRINTER_DRIVER = 1797,
                /// <summary>
                /// The print processor is unknown.
                /// </summary>
                ERROR_UNKNOWN_PRINTPROCESSOR = 1798,
                /// <summary>
                /// The specified separator file is invalid.
                /// </summary>
                ERROR_INVALID_SEPARATOR_FILE = 1799,
                /// <summary>
                /// The specified priority is invalid.
                /// </summary>
                ERROR_INVALID_PRIORITY = 1800,
                /// <summary>
                /// The printer name is invalid.
                /// </summary>
                ERROR_INVALID_PRINTER_NAME = 1801,
                /// <summary>
                /// The printer already exists.
                /// </summary>
                ERROR_PRINTER_ALREADY_EXISTS = 1802,
                /// <summary>
                /// The printer command is invalid.
                /// </summary>
                ERROR_INVALID_PRINTER_COMMAND = 1803,
                /// <summary>
                /// The specified datatype is invalid.
                /// </summary>
                ERROR_INVALID_DATATYPE = 1804,
                /// <summary>
                /// The environment specified is invalid.
                /// </summary>
                ERROR_INVALID_ENVIRONMENT = 1805,
                /// <summary>
                /// There are no more bindings.
                /// </summary>
                RPC_S_NO_MORE_BINDINGS = 1806,
                /// <summary>
                /// The account used is an interdomain trust account. Use your global user account or local user account to access this server.
                /// </summary>
                ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = 1807,
                /// <summary>
                /// The account used is a computer account. Use your global user account or local user account to access this server.
                /// </summary>
                ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT = 1808,
                /// <summary>
                /// The account used is a server trust account. Use your global user account or local user account to access this server.
                /// </summary>
                ERROR_NOLOGON_SERVER_TRUST_ACCOUNT = 1809,
                /// <summary>
                /// The name or security ID (SID) of the domain specified is inconsistent with the trust information for that domain.
                /// </summary>
                ERROR_DOMAIN_TRUST_INCONSISTENT = 1810,
                /// <summary>
                /// The server is in use and cannot be unloaded.
                /// </summary>
                ERROR_SERVER_HAS_OPEN_HANDLES = 1811,
                /// <summary>
                /// The specified image file did not contain a resource section.
                /// </summary>
                ERROR_RESOURCE_DATA_NOT_FOUND = 1812,
                /// <summary>
                /// The specified resource type cannot be found in the image file.
                /// </summary>
                ERROR_RESOURCE_TYPE_NOT_FOUND = 1813,
                /// <summary>
                /// The specified resource name cannot be found in the image file.
                /// </summary>
                ERROR_RESOURCE_NAME_NOT_FOUND = 1814,
                /// <summary>
                /// The specified resource language ID cannot be found in the image file.
                /// </summary>
                ERROR_RESOURCE_LANG_NOT_FOUND = 1815,
                /// <summary>
                /// Not enough quota is available to process this command.
                /// </summary>
                ERROR_NOT_ENOUGH_QUOTA = 1816,
                /// <summary>
                /// No interfaces have been registered.
                /// </summary>
                RPC_S_NO_INTERFACES = 1817,
                /// <summary>
                /// The remote procedure call was cancelled.
                /// </summary>
                RPC_S_CALL_CANCELLED = 1818,
                /// <summary>
                /// The binding handle does not contain all required information.
                /// </summary>
                RPC_S_BINDING_INCOMPLETE = 1819,
                /// <summary>
                /// A communications failure occurred during a remote procedure call.
                /// </summary>
                RPC_S_COMM_FAILURE = 1820,
                /// <summary>
                /// The requested authentication level is not supported.
                /// </summary>
                RPC_S_UNSUPPORTED_AUTHN_LEVEL = 1821,
                /// <summary>
                /// No principal name registered.
                /// </summary>
                RPC_S_NO_PRINC_NAME = 1822,
                /// <summary>
                /// The error specified is not a valid Windows RPC error code.
                /// </summary>
                RPC_S_NOT_RPC_ERROR = 1823,
                /// <summary>
                /// A UUID that is valid only on this computer has been allocated.
                /// </summary>
                RPC_S_UUID_LOCAL_ONLY = 1824,
                /// <summary>
                /// A security package specific error occurred.
                /// </summary>
                RPC_S_SEC_PKG_ERROR = 1825,
                /// <summary>
                /// Thread is not canceled.
                /// </summary>
                RPC_S_NOT_CANCELLED = 1826,
                /// <summary>
                /// Invalid operation on the encoding/decoding handle.
                /// </summary>
                RPC_X_INVALID_ES_ACTION = 1827,
                /// <summary>
                /// Incompatible version of the serializing package.
                /// </summary>
                RPC_X_WRONG_ES_VERSION = 1828,
                /// <summary>
                /// Incompatible version of the RPC stub.
                /// </summary>
                RPC_X_WRONG_STUB_VERSION = 1829,
                /// <summary>
                /// The RPC pipe object is invalid or corrupted.
                /// </summary>
                RPC_X_INVALID_PIPE_OBJECT = 1830,
                /// <summary>
                /// An invalid operation was attempted on an RPC pipe object.
                /// </summary>
                RPC_X_WRONG_PIPE_ORDER = 1831,
                /// <summary>
                /// Unsupported RPC pipe version.
                /// </summary>
                RPC_X_WRONG_PIPE_VERSION = 1832,
                /// <summary>
                /// HTTP proxy server rejected the connection because the cookie authentication failed.
                /// </summary>
                RPC_S_COOKIE_AUTH_FAILED = 1833,
                /// <summary>
                /// The group member was not found.
                /// </summary>
                RPC_S_GROUP_MEMBER_NOT_FOUND = 1898,
                /// <summary>
                /// The endpoint mapper database entry could not be created.
                /// </summary>
                EPT_S_CANT_CREATE = 1899,
                /// <summary>
                /// The object universal unique identifier (UUID) is the nil UUID.
                /// </summary>
                RPC_S_INVALID_OBJECT = 1900,
                /// <summary>
                /// The specified time is invalid.
                /// </summary>
                ERROR_INVALID_TIME = 1901,
                /// <summary>
                /// The specified form name is invalid.
                /// </summary>
                ERROR_INVALID_FORM_NAME = 1902,
                /// <summary>
                /// The specified form size is invalid.
                /// </summary>
                ERROR_INVALID_FORM_SIZE = 1903,
                /// <summary>
                /// The specified printer handle is already being waited on.
                /// </summary>
                ERROR_ALREADY_WAITING = 1904,
                /// <summary>
                /// The specified printer has been deleted.
                /// </summary>
                ERROR_PRINTER_DELETED = 1905,
                /// <summary>
                /// The state of the printer is invalid.
                /// </summary>
                ERROR_INVALID_PRINTER_STATE = 1906,
                /// <summary>
                /// The user's password must be changed before signing in.
                /// </summary>
                ERROR_PASSWORD_MUST_CHANGE = 1907,
                /// <summary>
                /// Could not find the domain controller for this domain.
                /// </summary>
                ERROR_DOMAIN_CONTROLLER_NOT_FOUND = 1908,
                /// <summary>
                /// The referenced account is currently locked out and may not be logged on to.
                /// </summary>
                ERROR_ACCOUNT_LOCKED_OUT = 1909,
                /// <summary>
                /// The object exporter specified was not found.
                /// </summary>
                OR_INVALID_OXID = 1910,
                /// <summary>
                /// The object specified was not found.
                /// </summary>
                OR_INVALID_OID = 1911,
                /// <summary>
                /// The object resolver set specified was not found.
                /// </summary>
                OR_INVALID_SET = 1912,
                /// <summary>
                /// Some data remains to be sent in the request buffer.
                /// </summary>
                RPC_S_SEND_INCOMPLETE = 1913,
                /// <summary>
                /// Invalid asynchronous remote procedure call handle.
                /// </summary>
                RPC_S_INVALID_ASYNC_HANDLE = 1914,
                /// <summary>
                /// Invalid asynchronous RPC call handle for this operation.
                /// </summary>
                RPC_S_INVALID_ASYNC_CALL = 1915,
                /// <summary>
                /// The RPC pipe object has already been closed.
                /// </summary>
                RPC_X_PIPE_CLOSED = 1916,
                /// <summary>
                /// The RPC call completed before all pipes were processed.
                /// </summary>
                RPC_X_PIPE_DISCIPLINE_ERROR = 1917,
                /// <summary>
                /// No more data is available from the RPC pipe.
                /// </summary>
                RPC_X_PIPE_EMPTY = 1918,
                /// <summary>
                /// No site name is available for this machine.
                /// </summary>
                ERROR_NO_SITENAME = 1919,
                /// <summary>
                /// The file cannot be accessed by the system.
                /// </summary>
                ERROR_CANT_ACCESS_FILE = 1920,
                /// <summary>
                /// The name of the file cannot be resolved by the system.
                /// </summary>
                ERROR_CANT_RESOLVE_FILENAME = 1921,
                /// <summary>
                /// The entry is not of the expected type.
                /// </summary>
                RPC_S_ENTRY_TYPE_MISMATCH = 1922,
                /// <summary>
                /// Not all object UUIDs could be exported to the specified entry.
                /// </summary>
                RPC_S_NOT_ALL_OBJS_EXPORTED = 1923,
                /// <summary>
                /// Interface could not be exported to the specified entry.
                /// </summary>
                RPC_S_INTERFACE_NOT_EXPORTED = 1924,
                /// <summary>
                /// The specified profile entry could not be added.
                /// </summary>
                RPC_S_PROFILE_NOT_ADDED = 1925,
                /// <summary>
                /// The specified profile element could not be added.
                /// </summary>
                RPC_S_PRF_ELT_NOT_ADDED = 1926,
                /// <summary>
                /// The specified profile element could not be removed.
                /// </summary>
                RPC_S_PRF_ELT_NOT_REMOVED = 1927,
                /// <summary>
                /// The group element could not be added.
                /// </summary>
                RPC_S_GRP_ELT_NOT_ADDED = 1928,
                /// <summary>
                /// The group element could not be removed.
                /// </summary>
                RPC_S_GRP_ELT_NOT_REMOVED = 1929,
                /// <summary>
                /// The printer driver is not compatible with a policy enabled on your computer that blocks NT 4.0 drivers.
                /// </summary>
                ERROR_KM_DRIVER_BLOCKED = 1930,
                /// <summary>
                /// The context has expired and can no longer be used.
                /// </summary>
                ERROR_CONTEXT_EXPIRED = 1931,
                /// <summary>
                /// The current user's delegated trust creation quota has been exceeded.
                /// </summary>
                ERROR_PER_USER_TRUST_QUOTA_EXCEEDED = 1932,
                /// <summary>
                /// The total delegated trust creation quota has been exceeded.
                /// </summary>
                ERROR_ALL_USER_TRUST_QUOTA_EXCEEDED = 1933,
                /// <summary>
                /// The current user's delegated trust deletion quota has been exceeded.
                /// </summary>
                ERROR_USER_DELETE_TRUST_QUOTA_EXCEEDED = 1934,
                /// <summary>
                /// The computer you are signing into is protected by an authentication firewall. The specified account is not allowed to authenticate to the computer.
                /// </summary>
                ERROR_AUTHENTICATION_FIREWALL_FAILED = 1935,
                /// <summary>
                /// Remote connections to the Print Spooler are blocked by a policy set on your machine.
                /// </summary>
                ERROR_REMOTE_PRINT_CONNECTIONS_BLOCKED = 1936,
                /// <summary>
                /// Authentication failed because NTLM authentication has been disabled.
                /// </summary>
                ERROR_NTLM_BLOCKED = 1937,
                /// <summary>
                /// Logon Failure: EAS policy requires that the user change their password before this operation can be performed.
                /// </summary>
                ERROR_PASSWORD_CHANGE_REQUIRED = 1938,
                /// <summary>
                /// The pixel format is invalid.
                /// </summary>
                ERROR_INVALID_PIXEL_FORMAT = 2000,
                /// <summary>
                /// The specified driver is invalid.
                /// </summary>
                ERROR_BAD_DRIVER = 2001,
                /// <summary>
                /// The window style or class attribute is invalid for this operation.
                /// </summary>
                ERROR_INVALID_WINDOW_STYLE = 2002,
                /// <summary>
                /// The requested metafile operation is not supported.
                /// </summary>
                ERROR_METAFILE_NOT_SUPPORTED = 2003,
                /// <summary>
                /// The requested transformation operation is not supported.
                /// </summary>
                ERROR_TRANSFORM_NOT_SUPPORTED = 2004,
                /// <summary>
                /// The requested clipping operation is not supported.
                /// </summary>
                ERROR_CLIPPING_NOT_SUPPORTED = 2005,
                /// <summary>
                /// The specified color management module is invalid.
                /// </summary>
                ERROR_INVALID_CMM = 2010,
                /// <summary>
                /// The specified color profile is invalid.
                /// </summary>
                ERROR_INVALID_PROFILE = 2011,
                /// <summary>
                /// The specified tag was not found.
                /// </summary>
                ERROR_TAG_NOT_FOUND = 2012,
                /// <summary>
                /// A required tag is not present.
                /// </summary>
                ERROR_TAG_NOT_PRESENT = 2013,
                /// <summary>
                /// The specified tag is already present.
                /// </summary>
                ERROR_DUPLICATE_TAG = 2014,
                /// <summary>
                /// The specified color profile is not associated with the specified device.
                /// </summary>
                ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE = 2015,
                /// <summary>
                /// The specified color profile was not found.
                /// </summary>
                ERROR_PROFILE_NOT_FOUND = 2016,
                /// <summary>
                /// The specified color space is invalid.
                /// </summary>
                ERROR_INVALID_COLORSPACE = 2017,
                /// <summary>
                /// Image Color Management is not enabled.
                /// </summary>
                ERROR_ICM_NOT_ENABLED = 2018,
                /// <summary>
                /// There was an error while deleting the color transform.
                /// </summary>
                ERROR_DELETING_ICM_XFORM = 2019,
                /// <summary>
                /// The specified color transform is invalid.
                /// </summary>
                ERROR_INVALID_TRANSFORM = 2020,
                /// <summary>
                /// The specified transform does not match the bitmap's color space.
                /// </summary>
                ERROR_COLORSPACE_MISMATCH = 2021,
                /// <summary>
                /// The specified named color index is not present in the profile.
                /// </summary>
                ERROR_INVALID_COLORINDEX = 2022,
                /// <summary>
                /// The specified profile is intended for a device of a different type than the specified device.
                /// </summary>
                ERROR_PROFILE_DOES_NOT_MATCH_DEVICE = 2023,
                /// <summary>
                /// The network connection was made successfully, but the user had to be prompted for a password other than the one originally specified.
                /// </summary>
                ERROR_CONNECTED_OTHER_PASSWORD = 2108,
                /// <summary>
                /// The network connection was made successfully using default credentials.
                /// </summary>
                ERROR_CONNECTED_OTHER_PASSWORD_DEFAULT = 2109,
                /// <summary>
                /// The specified username is invalid.
                /// </summary>
                ERROR_BAD_USERNAME = 2202,
                /// <summary>
                /// This network connection does not exist.
                /// </summary>
                ERROR_NOT_CONNECTED = 2250,
                /// <summary>
                /// This network connection has files open or requests pending.
                /// </summary>
                ERROR_OPEN_FILES = 2401,
                /// <summary>
                /// Active connections still exist.
                /// </summary>
                ERROR_ACTIVE_CONNECTIONS = 2402,
                /// <summary>
                /// The device is in use by an active process and cannot be disconnected.
                /// </summary>
                ERROR_DEVICE_IN_USE = 2404,
                /// <summary>
                /// The specified print monitor is unknown.
                /// </summary>
                ERROR_UNKNOWN_PRINT_MONITOR = 3000,
                /// <summary>
                /// The specified printer driver is currently in use.
                /// </summary>
                ERROR_PRINTER_DRIVER_IN_USE = 3001,
                /// <summary>
                /// The spool file was not found.
                /// </summary>
                ERROR_SPOOL_FILE_NOT_FOUND = 3002,
                /// <summary>
                /// A StartDocPrinter call was not issued.
                /// </summary>
                ERROR_SPL_NO_STARTDOC = 3003,
                /// <summary>
                /// An AddJob call was not issued.
                /// </summary>
                ERROR_SPL_NO_ADDJOB = 3004,
                /// <summary>
                /// The specified print processor has already been installed.
                /// </summary>
                ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 3005,
                /// <summary>
                /// The specified print monitor has already been installed.
                /// </summary>
                ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 3006,
                /// <summary>
                /// The specified print monitor does not have the required functions.
                /// </summary>
                ERROR_INVALID_PRINT_MONITOR = 3007,
                /// <summary>
                /// The specified print monitor is currently in use.
                /// </summary>
                ERROR_PRINT_MONITOR_IN_USE = 3008,
                /// <summary>
                /// The requested operation is not allowed when there are jobs queued to the printer.
                /// </summary>
                ERROR_PRINTER_HAS_JOBS_QUEUED = 3009,
                /// <summary>
                /// The requested operation is successful. Changes will not be effective until the system is rebooted.
                /// </summary>
                ERROR_SUCCESS_REBOOT_REQUIRED = 3010,
                /// <summary>
                /// The requested operation is successful. Changes will not be effective until the service is restarted.
                /// </summary>
                ERROR_SUCCESS_RESTART_REQUIRED = 3011,
                /// <summary>
                /// No printers were found.
                /// </summary>
                ERROR_PRINTER_NOT_FOUND = 3012,
                /// <summary>
                /// The printer driver is known to be unreliable.
                /// </summary>
                ERROR_PRINTER_DRIVER_WARNED = 3013,
                /// <summary>
                /// The printer driver is known to harm the system.
                /// </summary>
                ERROR_PRINTER_DRIVER_BLOCKED = 3014,
                /// <summary>
                /// The specified printer driver package is currently in use.
                /// </summary>
                ERROR_PRINTER_DRIVER_PACKAGE_IN_USE = 3015,
                /// <summary>
                /// Unable to find a core driver package that is required by the printer driver package.
                /// </summary>
                ERROR_CORE_DRIVER_PACKAGE_NOT_FOUND = 3016,
                /// <summary>
                /// The requested operation failed. A system reboot is required to roll back changes made.
                /// </summary>
                ERROR_FAIL_REBOOT_REQUIRED = 3017,
                /// <summary>
                /// The requested operation failed. A system reboot has been initiated to roll back changes made.
                /// </summary>
                ERROR_FAIL_REBOOT_INITIATED = 3018,
                /// <summary>
                /// The specified printer driver was not found on the system and needs to be downloaded.
                /// </summary>
                ERROR_PRINTER_DRIVER_DOWNLOAD_NEEDED = 3019,
                /// <summary>
                /// The requested print job has failed to print. A print system update requires the job to be resubmitted.
                /// </summary>
                ERROR_PRINT_JOB_RESTART_REQUIRED = 3020,
                /// <summary>
                /// The printer driver does not contain a valid manifest, or contains too many manifests.
                /// </summary>
                ERROR_INVALID_PRINTER_DRIVER_MANIFEST = 3021,
                /// <summary>
                /// The specified printer cannot be shared.
                /// </summary>
                ERROR_PRINTER_NOT_SHAREABLE = 3022,
                /// <summary>
                /// The operation was paused.
                /// </summary>
                ERROR_REQUEST_PAUSED = 3050,
                /// <summary>
                /// Reissue the given operation as a cached IO operation.
                /// </summary>
                ERROR_IO_REISSUE_AS_CACHED = 3950,
                /// <summary>
                /// WINS encountered an error while processing the command.
                /// </summary>
                ERROR_WINS_INTERNAL = 4000,
                /// <summary>
                /// The local WINS cannot be deleted.
                /// </summary>
                ERROR_CAN_NOT_DEL_LOCAL_WINS = 4001,
                /// <summary>
                /// The importation from the file failed.
                /// </summary>
                ERROR_STATIC_INIT = 4002,
                /// <summary>
                /// The backup failed. Was a full backup done before?
                /// </summary>
                ERROR_INC_BACKUP = 4003,
                /// <summary>
                /// The backup failed. Check the directory to which you are backing the database.
                /// </summary>
                ERROR_FULL_BACKUP = 4004,
                /// <summary>
                /// The name does not exist in the WINS database.
                /// </summary>
                ERROR_REC_NON_EXISTENT = 4005,
                /// <summary>
                /// Replication with a nonconfigured partner is not allowed.
                /// </summary>
                ERROR_RPL_NOT_ALLOWED = 4006,
                /// <summary>
                /// The version of the supplied content information is not supported.
                /// </summary>
                PEERDIST_ERROR_CONTENTINFO_VERSION_UNSUPPORTED = 4050,
                /// <summary>
                /// The supplied content information is malformed.
                /// </summary>
                PEERDIST_ERROR_CANNOT_PARSE_CONTENTINFO = 4051,
                /// <summary>
                /// The requested data cannot be found in local or peer caches.
                /// </summary>
                PEERDIST_ERROR_MISSING_DATA = 4052,
                /// <summary>
                /// No more data is available or required.
                /// </summary>
                PEERDIST_ERROR_NO_MORE = 4053,
                /// <summary>
                /// The supplied object has not been initialized.
                /// </summary>
                PEERDIST_ERROR_NOT_INITIALIZED = 4054,
                /// <summary>
                /// The supplied object has already been initialized.
                /// </summary>
                PEERDIST_ERROR_ALREADY_INITIALIZED = 4055,
                /// <summary>
                /// A shutdown operation is already in progress.
                /// </summary>
                PEERDIST_ERROR_SHUTDOWN_IN_PROGRESS = 4056,
                /// <summary>
                /// The supplied object has already been invalidated.
                /// </summary>
                PEERDIST_ERROR_INVALIDATED = 4057,
                /// <summary>
                /// An element already exists and was not replaced.
                /// </summary>
                PEERDIST_ERROR_ALREADY_EXISTS = 4058,
                /// <summary>
                /// Can not cancel the requested operation as it has already been completed.
                /// </summary>
                PEERDIST_ERROR_OPERATION_NOTFOUND = 4059,
                /// <summary>
                /// Can not perform the reqested operation because it has already been carried out.
                /// </summary>
                PEERDIST_ERROR_ALREADY_COMPLETED = 4060,
                /// <summary>
                /// An operation accessed data beyond the bounds of valid data.
                /// </summary>
                PEERDIST_ERROR_OUT_OF_BOUNDS = 4061,
                /// <summary>
                /// The requested version is not supported.
                /// </summary>
                PEERDIST_ERROR_VERSION_UNSUPPORTED = 4062,
                /// <summary>
                /// A configuration value is invalid.
                /// </summary>
                PEERDIST_ERROR_INVALID_CONFIGURATION = 4063,
                /// <summary>
                /// The SKU is not licensed.
                /// </summary>
                PEERDIST_ERROR_NOT_LICENSED = 4064,
                /// <summary>
                /// PeerDist Service is still initializing and will be available shortly.
                /// </summary>
                PEERDIST_ERROR_SERVICE_UNAVAILABLE = 4065,
                /// <summary>
                /// Communication with one or more computers will be temporarily blocked due to recent errors.
                /// </summary>
                PEERDIST_ERROR_TRUST_FAILURE = 4066,
                /// <summary>
                /// The DHCP client has obtained an IP address that is already in use on the network. The local interface will be disabled until the DHCP client can obtain a new address.
                /// </summary>
                ERROR_DHCP_ADDRESS_CONFLICT = 4100,
                /// <summary>
                /// The GUID passed was not recognized as valid by a WMI data provider.
                /// </summary>
                ERROR_WMI_GUID_NOT_FOUND = 4200,
                /// <summary>
                /// The instance name passed was not recognized as valid by a WMI data provider.
                /// </summary>
                ERROR_WMI_INSTANCE_NOT_FOUND = 4201,
                /// <summary>
                /// The data item ID passed was not recognized as valid by a WMI data provider.
                /// </summary>
                ERROR_WMI_ITEMID_NOT_FOUND = 4202,
                /// <summary>
                /// The WMI request could not be completed and should be retried.
                /// </summary>
                ERROR_WMI_TRY_AGAIN = 4203,
                /// <summary>
                /// The WMI data provider could not be located.
                /// </summary>
                ERROR_WMI_DP_NOT_FOUND = 4204,
                /// <summary>
                /// The WMI data provider references an instance set that has not been registered.
                /// </summary>
                ERROR_WMI_UNRESOLVED_INSTANCE_REF = 4205,
                /// <summary>
                /// The WMI data block or event notification has already been enabled.
                /// </summary>
                ERROR_WMI_ALREADY_ENABLED = 4206,
                /// <summary>
                /// The WMI data block is no longer available.
                /// </summary>
                ERROR_WMI_GUID_DISCONNECTED = 4207,
                /// <summary>
                /// The WMI data service is not available.
                /// </summary>
                ERROR_WMI_SERVER_UNAVAILABLE = 4208,
                /// <summary>
                /// The WMI data provider failed to carry out the request.
                /// </summary>
                ERROR_WMI_DP_FAILED = 4209,
                /// <summary>
                /// The WMI MOF information is not valid.
                /// </summary>
                ERROR_WMI_INVALID_MOF = 4210,
                /// <summary>
                /// The WMI registration information is not valid.
                /// </summary>
                ERROR_WMI_INVALID_REGINFO = 4211,
                /// <summary>
                /// The WMI data block or event notification has already been disabled.
                /// </summary>
                ERROR_WMI_ALREADY_DISABLED = 4212,
                /// <summary>
                /// The WMI data item or data block is read only.
                /// </summary>
                ERROR_WMI_READ_ONLY = 4213,
                /// <summary>
                /// The WMI data item or data block could not be changed.
                /// </summary>
                ERROR_WMI_SET_FAILURE = 4214,
                /// <summary>
                /// This operation is only valid in the context of an app container.
                /// </summary>
                ERROR_NOT_APPCONTAINER = 4250,
                /// <summary>
                /// This application can only run in the context of an app container.
                /// </summary>
                ERROR_APPCONTAINER_REQUIRED = 4251,
                /// <summary>
                /// This functionality is not supported in the context of an app container.
                /// </summary>
                ERROR_NOT_SUPPORTED_IN_APPCONTAINER = 4252,
                /// <summary>
                /// The length of the SID supplied is not a valid length for app container SIDs.
                /// </summary>
                ERROR_INVALID_PACKAGE_SID_LENGTH = 4253,
                /// <summary>
                /// The media identifier does not represent a valid medium.
                /// </summary>
                ERROR_INVALID_MEDIA = 4300,
                /// <summary>
                /// The library identifier does not represent a valid library.
                /// </summary>
                ERROR_INVALID_LIBRARY = 4301,
                /// <summary>
                /// The media pool identifier does not represent a valid media pool.
                /// </summary>
                ERROR_INVALID_MEDIA_POOL = 4302,
                /// <summary>
                /// The drive and medium are not compatible or exist in different libraries.
                /// </summary>
                ERROR_DRIVE_MEDIA_MISMATCH = 4303,
                /// <summary>
                /// The medium currently exists in an offline library and must be online to perform this operation.
                /// </summary>
                ERROR_MEDIA_OFFLINE = 4304,
                /// <summary>
                /// The operation cannot be performed on an offline library.
                /// </summary>
                ERROR_LIBRARY_OFFLINE = 4305,
                /// <summary>
                /// The library, drive, or media pool is empty.
                /// </summary>
                ERROR_EMPTY = 4306,
                /// <summary>
                /// The library, drive, or media pool must be empty to perform this operation.
                /// </summary>
                ERROR_NOT_EMPTY = 4307,
                /// <summary>
                /// No media is currently available in this media pool or library.
                /// </summary>
                ERROR_MEDIA_UNAVAILABLE = 4308,
                /// <summary>
                /// A resource required for this operation is disabled.
                /// </summary>
                ERROR_RESOURCE_DISABLED = 4309,
                /// <summary>
                /// The media identifier does not represent a valid cleaner.
                /// </summary>
                ERROR_INVALID_CLEANER = 4310,
                /// <summary>
                /// The drive cannot be cleaned or does not support cleaning.
                /// </summary>
                ERROR_UNABLE_TO_CLEAN = 4311,
                /// <summary>
                /// The object identifier does not represent a valid object.
                /// </summary>
                ERROR_OBJECT_NOT_FOUND = 4312,
                /// <summary>
                /// Unable to read from or write to the database.
                /// </summary>
                ERROR_DATABASE_FAILURE = 4313,
                /// <summary>
                /// The database is full.
                /// </summary>
                ERROR_DATABASE_FULL = 4314,
                /// <summary>
                /// The medium is not compatible with the device or media pool.
                /// </summary>
                ERROR_MEDIA_INCOMPATIBLE = 4315,
                /// <summary>
                /// The resource required for this operation does not exist.
                /// </summary>
                ERROR_RESOURCE_NOT_PRESENT = 4316,
                /// <summary>
                /// The operation identifier is not valid.
                /// </summary>
                ERROR_INVALID_OPERATION = 4317,
                /// <summary>
                /// The media is not mounted or ready for use.
                /// </summary>
                ERROR_MEDIA_NOT_AVAILABLE = 4318,
                /// <summary>
                /// The device is not ready for use.
                /// </summary>
                ERROR_DEVICE_NOT_AVAILABLE = 4319,
                /// <summary>
                /// The operator or administrator has refused the request.
                /// </summary>
                ERROR_REQUEST_REFUSED = 4320,
                /// <summary>
                /// The drive identifier does not represent a valid drive.
                /// </summary>
                ERROR_INVALID_DRIVE_OBJECT = 4321,
                /// <summary>
                /// Library is full. No slot is available for use.
                /// </summary>
                ERROR_LIBRARY_FULL = 4322,
                /// <summary>
                /// The transport cannot access the medium.
                /// </summary>
                ERROR_MEDIUM_NOT_ACCESSIBLE = 4323,
                /// <summary>
                /// Unable to load the medium into the drive.
                /// </summary>
                ERROR_UNABLE_TO_LOAD_MEDIUM = 4324,
                /// <summary>
                /// Unable to retrieve the drive status.
                /// </summary>
                ERROR_UNABLE_TO_INVENTORY_DRIVE = 4325,
                /// <summary>
                /// Unable to retrieve the slot status.
                /// </summary>
                ERROR_UNABLE_TO_INVENTORY_SLOT = 4326,
                /// <summary>
                /// Unable to retrieve status about the transport.
                /// </summary>
                ERROR_UNABLE_TO_INVENTORY_TRANSPORT = 4327,
                /// <summary>
                /// Cannot use the transport because it is already in use.
                /// </summary>
                ERROR_TRANSPORT_FULL = 4328,
                /// <summary>
                /// Unable to open or close the inject/eject port.
                /// </summary>
                ERROR_CONTROLLING_IEPORT = 4329,
                /// <summary>
                /// Unable to eject the medium because it is in a drive.
                /// </summary>
                ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA = 4330,
                /// <summary>
                /// A cleaner slot is already reserved.
                /// </summary>
                ERROR_CLEANER_SLOT_SET = 4331,
                /// <summary>
                /// A cleaner slot is not reserved.
                /// </summary>
                ERROR_CLEANER_SLOT_NOT_SET = 4332,
                /// <summary>
                /// The cleaner cartridge has performed the maximum number of drive cleanings.
                /// </summary>
                ERROR_CLEANER_CARTRIDGE_SPENT = 4333,
                /// <summary>
                /// Unexpected on-medium identifier.
                /// </summary>
                ERROR_UNEXPECTED_OMID = 4334,
                /// <summary>
                /// The last remaining item in this group or resource cannot be deleted.
                /// </summary>
                ERROR_CANT_DELETE_LAST_ITEM = 4335,
                /// <summary>
                /// The message provided exceeds the maximum size allowed for this parameter.
                /// </summary>
                ERROR_MESSAGE_EXCEEDS_MAX_SIZE = 4336,
                /// <summary>
                /// The volume contains system or paging files.
                /// </summary>
                ERROR_VOLUME_CONTAINS_SYS_FILES = 4337,
                /// <summary>
                /// The media type cannot be removed from this library since at least one drive in the library reports it can support this media type.
                /// </summary>
                ERROR_INDIGENOUS_TYPE = 4338,
                /// <summary>
                /// This offline media cannot be mounted on this system since no enabled drives are present which can be used.
                /// </summary>
                ERROR_NO_SUPPORTING_DRIVES = 4339,
                /// <summary>
                /// A cleaner cartridge is present in the tape library.
                /// </summary>
                ERROR_CLEANER_CARTRIDGE_INSTALLED = 4340,
                /// <summary>
                /// Cannot use the inject/eject port because it is not empty.
                /// </summary>
                ERROR_IEPORT_FULL = 4341,
                /// <summary>
                /// This file is currently not available for use on this computer.
                /// </summary>
                ERROR_FILE_OFFLINE = 4350,
                /// <summary>
                /// The remote storage service is not operational at this time.
                /// </summary>
                ERROR_REMOTE_STORAGE_NOT_ACTIVE = 4351,
                /// <summary>
                /// The remote storage service encountered a media error.
                /// </summary>
                ERROR_REMOTE_STORAGE_MEDIA_ERROR = 4352,
                /// <summary>
                /// The file or directory is not a reparse point.
                /// </summary>
                ERROR_NOT_A_REPARSE_POINT = 4390,
                /// <summary>
                /// The reparse point attribute cannot be set because it conflicts with an existing attribute.
                /// </summary>
                ERROR_REPARSE_ATTRIBUTE_CONFLICT = 4391,
                /// <summary>
                /// The data present in the reparse point buffer is invalid.
                /// </summary>
                ERROR_INVALID_REPARSE_DATA = 4392,
                /// <summary>
                /// The tag present in the reparse point buffer is invalid.
                /// </summary>
                ERROR_REPARSE_TAG_INVALID = 4393,
                /// <summary>
                /// There is a mismatch between the tag specified in the request and the tag present in the reparse point.
                /// </summary>
                ERROR_REPARSE_TAG_MISMATCH = 4394,
                /// <summary>
                /// Fast Cache data not found.
                /// </summary>
                ERROR_APP_DATA_NOT_FOUND = 4400,
                /// <summary>
                /// Fast Cache data expired.
                /// </summary>
                ERROR_APP_DATA_EXPIRED = 4401,
                /// <summary>
                /// Fast Cache data corrupt.
                /// </summary>
                ERROR_APP_DATA_CORRUPT = 4402,
                /// <summary>
                /// Fast Cache data has exceeded its max size and cannot be updated.
                /// </summary>
                ERROR_APP_DATA_LIMIT_EXCEEDED = 4403,
                /// <summary>
                /// Fast Cache has been ReArmed and requires a reboot until it can be updated.
                /// </summary>
                ERROR_APP_DATA_REBOOT_REQUIRED = 4404,
                /// <summary>
                /// Secure Boot detected that rollback of protected data has been attempted.
                /// </summary>
                ERROR_SECUREBOOT_ROLLBACK_DETECTED = 4420,
                /// <summary>
                /// The value is protected by Secure Boot policy and cannot be modified or deleted.
                /// </summary>
                ERROR_SECUREBOOT_POLICY_VIOLATION = 4421,
                /// <summary>
                /// The Secure Boot policy is invalid.
                /// </summary>
                ERROR_SECUREBOOT_INVALID_POLICY = 4422,
                /// <summary>
                /// A new Secure Boot policy did not contain the current publisher on its update list.
                /// </summary>
                ERROR_SECUREBOOT_POLICY_PUBLISHER_NOT_FOUND = 4423,
                /// <summary>
                /// The Secure Boot policy is either not signed or is signed by a non-trusted signer.
                /// </summary>
                ERROR_SECUREBOOT_POLICY_NOT_SIGNED = 4424,
                /// <summary>
                /// Secure Boot is not enabled on this machine.
                /// </summary>
                ERROR_SECUREBOOT_NOT_ENABLED = 4425,
                /// <summary>
                /// Secure Boot requires that certain files and drivers are not replaced by other files or drivers.
                /// </summary>
                ERROR_SECUREBOOT_FILE_REPLACED = 4426,
                /// <summary>
                /// The copy offload read operation is not supported by a filter.
                /// </summary>
                ERROR_OFFLOAD_READ_FLT_NOT_SUPPORTED = 4440,
                /// <summary>
                /// The copy offload write operation is not supported by a filter.
                /// </summary>
                ERROR_OFFLOAD_WRITE_FLT_NOT_SUPPORTED = 4441,
                /// <summary>
                /// The copy offload read operation is not supported for the file.
                /// </summary>
                ERROR_OFFLOAD_READ_FILE_NOT_SUPPORTED = 4442,
                /// <summary>
                /// The copy offload write operation is not supported for the file.
                /// </summary>
                ERROR_OFFLOAD_WRITE_FILE_NOT_SUPPORTED = 4443,
                /// <summary>
                /// Single Instance Storage is not available on this volume.
                /// </summary>
                ERROR_VOLUME_NOT_SIS_ENABLED = 4500,
                /// <summary>
                /// The operation cannot be completed because other resources are dependent on this resource.
                /// </summary>
                ERROR_DEPENDENT_RESOURCE_EXISTS = 5001,
                /// <summary>
                /// The cluster resource dependency cannot be found.
                /// </summary>
                ERROR_DEPENDENCY_NOT_FOUND = 5002,
                /// <summary>
                /// The cluster resource cannot be made dependent on the specified resource because it is already dependent.
                /// </summary>
                ERROR_DEPENDENCY_ALREADY_EXISTS = 5003,
                /// <summary>
                /// The cluster resource is not online.
                /// </summary>
                ERROR_RESOURCE_NOT_ONLINE = 5004,
                /// <summary>
                /// A cluster node is not available for this operation.
                /// </summary>
                ERROR_HOST_NODE_NOT_AVAILABLE = 5005,
                /// <summary>
                /// The cluster resource is not available.
                /// </summary>
                ERROR_RESOURCE_NOT_AVAILABLE = 5006,
                /// <summary>
                /// The cluster resource could not be found.
                /// </summary>
                ERROR_RESOURCE_NOT_FOUND = 5007,
                /// <summary>
                /// The cluster is being shut down.
                /// </summary>
                ERROR_SHUTDOWN_CLUSTER = 5008,
                /// <summary>
                /// A cluster node cannot be evicted from the cluster unless the node is down or it is the last node.
                /// </summary>
                ERROR_CANT_EVICT_ACTIVE_NODE = 5009,
                /// <summary>
                /// The object already exists.
                /// </summary>
                ERROR_OBJECT_ALREADY_EXISTS = 5010,
                /// <summary>
                /// The object is already in the list.
                /// </summary>
                ERROR_OBJECT_IN_LIST = 5011,
                /// <summary>
                /// The cluster group is not available for any new requests.
                /// </summary>
                ERROR_GROUP_NOT_AVAILABLE = 5012,
                /// <summary>
                /// The cluster group could not be found.
                /// </summary>
                ERROR_GROUP_NOT_FOUND = 5013,
                /// <summary>
                /// The operation could not be completed because the cluster group is not online.
                /// </summary>
                ERROR_GROUP_NOT_ONLINE = 5014,
                /// <summary>
                /// The operation failed because either the specified cluster node is not the owner of the resource, or the node is not a possible owner of the resource.
                /// </summary>
                ERROR_HOST_NODE_NOT_RESOURCE_OWNER = 5015,
                /// <summary>
                /// The operation failed because either the specified cluster node is not the owner of the group, or the node is not a possible owner of the group.
                /// </summary>
                ERROR_HOST_NODE_NOT_GROUP_OWNER = 5016,
                /// <summary>
                /// The cluster resource could not be created in the specified resource monitor.
                /// </summary>
                ERROR_RESMON_CREATE_FAILED = 5017,
                /// <summary>
                /// The cluster resource could not be brought online by the resource monitor.
                /// </summary>
                ERROR_RESMON_ONLINE_FAILED = 5018,
                /// <summary>
                /// The operation could not be completed because the cluster resource is online.
                /// </summary>
                ERROR_RESOURCE_ONLINE = 5019,
                /// <summary>
                /// The cluster resource could not be deleted or brought offline because it is the quorum resource.
                /// </summary>
                ERROR_QUORUM_RESOURCE = 5020,
                /// <summary>
                /// The cluster could not make the specified resource a quorum resource because it is not capable of being a quorum resource.
                /// </summary>
                ERROR_NOT_QUORUM_CAPABLE = 5021,
                /// <summary>
                /// The cluster software is shutting down.
                /// </summary>
                ERROR_CLUSTER_SHUTTING_DOWN = 5022,
                /// <summary>
                /// The group or resource is not in the correct state to perform the requested operation.
                /// </summary>
                ERROR_INVALID_STATE = 5023,
                /// <summary>
                /// The properties were stored but not all changes will take effect until the next time the resource is brought online.
                /// </summary>
                ERROR_RESOURCE_PROPERTIES_STORED = 5024,
                /// <summary>
                /// The cluster could not make the specified resource a quorum resource because it does not belong to a shared storage class.
                /// </summary>
                ERROR_NOT_QUORUM_CLASS = 5025,
                /// <summary>
                /// The cluster resource could not be deleted since it is a core resource.
                /// </summary>
                ERROR_CORE_RESOURCE = 5026,
                /// <summary>
                /// The quorum resource failed to come online.
                /// </summary>
                ERROR_QUORUM_RESOURCE_ONLINE_FAILED = 5027,
                /// <summary>
                /// The quorum log could not be created or mounted successfully.
                /// </summary>
                ERROR_QUORUMLOG_OPEN_FAILED = 5028,
                /// <summary>
                /// The cluster log is corrupt.
                /// </summary>
                ERROR_CLUSTERLOG_CORRUPT = 5029,
                /// <summary>
                /// The record could not be written to the cluster log since it exceeds the maximum size.
                /// </summary>
                ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE = 5030,
                /// <summary>
                /// The cluster log exceeds its maximum size.
                /// </summary>
                ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE = 5031,
                /// <summary>
                /// No checkpoint record was found in the cluster log.
                /// </summary>
                ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND = 5032,
                /// <summary>
                /// The minimum required disk space needed for logging is not available.
                /// </summary>
                ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE = 5033,
                /// <summary>
                /// The cluster node failed to take control of the quorum resource because the resource is owned by another active node.
                /// </summary>
                ERROR_QUORUM_OWNER_ALIVE = 5034,
                /// <summary>
                /// A cluster network is not available for this operation.
                /// </summary>
                ERROR_NETWORK_NOT_AVAILABLE = 5035,
                /// <summary>
                /// A cluster node is not available for this operation.
                /// </summary>
                ERROR_NODE_NOT_AVAILABLE = 5036,
                /// <summary>
                /// All cluster nodes must be running to perform this operation.
                /// </summary>
                ERROR_ALL_NODES_NOT_AVAILABLE = 5037,
                /// <summary>
                /// A cluster resource failed.
                /// </summary>
                ERROR_RESOURCE_FAILED = 5038,
                /// <summary>
                /// The cluster node is not valid.
                /// </summary>
                ERROR_CLUSTER_INVALID_NODE = 5039,
                /// <summary>
                /// The cluster node already exists.
                /// </summary>
                ERROR_CLUSTER_NODE_EXISTS = 5040,
                /// <summary>
                /// A node is in the process of joining the cluster.
                /// </summary>
                ERROR_CLUSTER_JOIN_IN_PROGRESS = 5041,
                /// <summary>
                /// The cluster node was not found.
                /// </summary>
                ERROR_CLUSTER_NODE_NOT_FOUND = 5042,
                /// <summary>
                /// The cluster local node information was not found.
                /// </summary>
                ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND = 5043,
                /// <summary>
                /// The cluster network already exists.
                /// </summary>
                ERROR_CLUSTER_NETWORK_EXISTS = 5044,
                /// <summary>
                /// The cluster network was not found.
                /// </summary>
                ERROR_CLUSTER_NETWORK_NOT_FOUND = 5045,
                /// <summary>
                /// The cluster network interface already exists.
                /// </summary>
                ERROR_CLUSTER_NETINTERFACE_EXISTS = 5046,
                /// <summary>
                /// The cluster network interface was not found.
                /// </summary>
                ERROR_CLUSTER_NETINTERFACE_NOT_FOUND = 5047,
                /// <summary>
                /// The cluster request is not valid for this object.
                /// </summary>
                ERROR_CLUSTER_INVALID_REQUEST = 5048,
                /// <summary>
                /// The cluster network provider is not valid.
                /// </summary>
                ERROR_CLUSTER_INVALID_NETWORK_PROVIDER = 5049,
                /// <summary>
                /// The cluster node is down.
                /// </summary>
                ERROR_CLUSTER_NODE_DOWN = 5050,
                /// <summary>
                /// The cluster node is not reachable.
                /// </summary>
                ERROR_CLUSTER_NODE_UNREACHABLE = 5051,
                /// <summary>
                /// The cluster node is not a member of the cluster.
                /// </summary>
                ERROR_CLUSTER_NODE_NOT_MEMBER = 5052,
                /// <summary>
                /// A cluster join operation is not in progress.
                /// </summary>
                ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS = 5053,
                /// <summary>
                /// The cluster network is not valid.
                /// </summary>
                ERROR_CLUSTER_INVALID_NETWORK = 5054,
                /// <summary>
                /// The cluster node is up.
                /// </summary>
                ERROR_CLUSTER_NODE_UP = 5056,
                /// <summary>
                /// The cluster IP address is already in use.
                /// </summary>
                ERROR_CLUSTER_IPADDR_IN_USE = 5057,
                /// <summary>
                /// The cluster node is not paused.
                /// </summary>
                ERROR_CLUSTER_NODE_NOT_PAUSED = 5058,
                /// <summary>
                /// No cluster security context is available.
                /// </summary>
                ERROR_CLUSTER_NO_SECURITY_CONTEXT = 5059,
                /// <summary>
                /// The cluster network is not configured for internal cluster communication.
                /// </summary>
                ERROR_CLUSTER_NETWORK_NOT_INTERNAL = 5060,
                /// <summary>
                /// The cluster node is already up.
                /// </summary>
                ERROR_CLUSTER_NODE_ALREADY_UP = 5061,
                /// <summary>
                /// The cluster node is already down.
                /// </summary>
                ERROR_CLUSTER_NODE_ALREADY_DOWN = 5062,
                /// <summary>
                /// The cluster network is already online.
                /// </summary>
                ERROR_CLUSTER_NETWORK_ALREADY_ONLINE = 5063,
                /// <summary>
                /// The cluster network is already offline.
                /// </summary>
                ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE = 5064,
                /// <summary>
                /// The cluster node is already a member of the cluster.
                /// </summary>
                ERROR_CLUSTER_NODE_ALREADY_MEMBER = 5065,
                /// <summary>
                /// The cluster network is the only one configured for internal cluster communication between two or more active cluster nodes. The internal communication capability cannot be removed from the network.
                /// </summary>
                ERROR_CLUSTER_LAST_INTERNAL_NETWORK = 5066,
                /// <summary>
                /// One or more cluster resources depend on the network to provide service to clients. The client access capability cannot be removed from the network.
                /// </summary>
                ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS = 5067,
                /// <summary>
                /// This operation cannot be performed on the cluster resource as it the quorum resource. You may not bring the quorum resource offline or modify its possible owners list.
                /// </summary>
                ERROR_INVALID_OPERATION_ON_QUORUM = 5068,
                /// <summary>
                /// The cluster quorum resource is not allowed to have any dependencies.
                /// </summary>
                ERROR_DEPENDENCY_NOT_ALLOWED = 5069,
                /// <summary>
                /// The cluster node is paused.
                /// </summary>
                ERROR_CLUSTER_NODE_PAUSED = 5070,
                /// <summary>
                /// The cluster resource cannot be brought online. The owner node cannot run this resource.
                /// </summary>
                ERROR_NODE_CANT_HOST_RESOURCE = 5071,
                /// <summary>
                /// The cluster node is not ready to perform the requested operation.
                /// </summary>
                ERROR_CLUSTER_NODE_NOT_READY = 5072,
                /// <summary>
                /// The cluster node is shutting down.
                /// </summary>
                ERROR_CLUSTER_NODE_SHUTTING_DOWN = 5073,
                /// <summary>
                /// The cluster join operation was aborted.
                /// </summary>
                ERROR_CLUSTER_JOIN_ABORTED = 5074,
                /// <summary>
                /// The cluster join operation failed due to incompatible software versions between the joining node and its sponsor.
                /// </summary>
                ERROR_CLUSTER_INCOMPATIBLE_VERSIONS = 5075,
                /// <summary>
                /// This resource cannot be created because the cluster has reached the limit on the number of resources it can monitor.
                /// </summary>
                ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED = 5076,
                /// <summary>
                /// The system configuration changed during the cluster join or form operation. The join or form operation was aborted.
                /// </summary>
                ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED = 5077,
                /// <summary>
                /// The specified resource type was not found.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND = 5078,
                /// <summary>
                /// The specified node does not support a resource of this type. This may be due to version inconsistencies or due to the absence of the resource DLL on this node.
                /// </summary>
                ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED = 5079,
                /// <summary>
                /// The specified resource name is not supported by this resource DLL. This may be due to a bad (or changed) name supplied to the resource DLL.
                /// </summary>
                ERROR_CLUSTER_RESNAME_NOT_FOUND = 5080,
                /// <summary>
                /// No authentication package could be registered with the RPC server.
                /// </summary>
                ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED = 5081,
                /// <summary>
                /// You cannot bring the group online because the owner of the group is not in the preferred list for the group. To change the owner node for the group, move the group.
                /// </summary>
                ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST = 5082,
                /// <summary>
                /// The join operation failed because the cluster database sequence number has changed or is incompatible with the locker node. This may happen during a join operation if the cluster database was changing during the join.
                /// </summary>
                ERROR_CLUSTER_DATABASE_SEQMISMATCH = 5083,
                /// <summary>
                /// The resource monitor will not allow the fail operation to be performed while the resource is in its current state. This may happen if the resource is in a pending state.
                /// </summary>
                ERROR_RESMON_INVALID_STATE = 5084,
                /// <summary>
                /// A non locker code got a request to reserve the lock for making global updates.
                /// </summary>
                ERROR_CLUSTER_GUM_NOT_LOCKER = 5085,
                /// <summary>
                /// The quorum disk could not be located by the cluster service.
                /// </summary>
                ERROR_QUORUM_DISK_NOT_FOUND = 5086,
                /// <summary>
                /// The backed up cluster database is possibly corrupt.
                /// </summary>
                ERROR_DATABASE_BACKUP_CORRUPT = 5087,
                /// <summary>
                /// A DFS root already exists in this cluster node.
                /// </summary>
                ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT = 5088,
                /// <summary>
                /// An attempt to modify a resource property failed because it conflicts with another existing property.
                /// </summary>
                ERROR_RESOURCE_PROPERTY_UNCHANGEABLE = 5089,
                /// <summary>
                /// An operation was attempted that is incompatible with the current membership state of the node.
                /// </summary>
                ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE = 5890,
                /// <summary>
                /// The quorum resource does not contain the quorum log.
                /// </summary>
                ERROR_CLUSTER_QUORUMLOG_NOT_FOUND = 5891,
                /// <summary>
                /// The membership engine requested shutdown of the cluster service on this node.
                /// </summary>
                ERROR_CLUSTER_MEMBERSHIP_HALT = 5892,
                /// <summary>
                /// The join operation failed because the cluster instance ID of the joining node does not match the cluster instance ID of the sponsor node.
                /// </summary>
                ERROR_CLUSTER_INSTANCE_ID_MISMATCH = 5893,
                /// <summary>
                /// A matching cluster network for the specified IP address could not be found.
                /// </summary>
                ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP = 5894,
                /// <summary>
                /// The actual data type of the property did not match the expected data type of the property.
                /// </summary>
                ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH = 5895,
                /// <summary>
                /// The cluster node was evicted from the cluster successfully, but the node was not cleaned up. To determine what cleanup steps failed and how to recover, see the Failover Clustering application event log using Event Viewer.
                /// </summary>
                ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP = 5896,
                /// <summary>
                /// Two or more parameter values specified for a resource's properties are in conflict.
                /// </summary>
                ERROR_CLUSTER_PARAMETER_MISMATCH = 5897,
                /// <summary>
                /// This computer cannot be made a member of a cluster.
                /// </summary>
                ERROR_NODE_CANNOT_BE_CLUSTERED = 5898,
                /// <summary>
                /// This computer cannot be made a member of a cluster because it does not have the correct version of Windows installed.
                /// </summary>
                ERROR_CLUSTER_WRONG_OS_VERSION = 5899,
                /// <summary>
                /// A cluster cannot be created with the specified cluster name because that cluster name is already in use. Specify a different name for the cluster.
                /// </summary>
                ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME = 5900,
                /// <summary>
                /// The cluster configuration action has already been committed.
                /// </summary>
                ERROR_CLUSCFG_ALREADY_COMMITTED = 5901,
                /// <summary>
                /// The cluster configuration action could not be rolled back.
                /// </summary>
                ERROR_CLUSCFG_ROLLBACK_FAILED = 5902,
                /// <summary>
                /// The drive letter assigned to a system disk on one node conflicted with the drive letter assigned to a disk on another node.
                /// </summary>
                ERROR_CLUSCFG_SYSTEM_DISK_DRIVE_LETTER_CONFLICT = 5903,
                /// <summary>
                /// One or more nodes in the cluster are running a version of Windows that does not support this operation.
                /// </summary>
                ERROR_CLUSTER_OLD_VERSION = 5904,
                /// <summary>
                /// The name of the corresponding computer account doesn't match the Network Name for this resource.
                /// </summary>
                ERROR_CLUSTER_MISMATCHED_COMPUTER_ACCT_NAME = 5905,
                /// <summary>
                /// No network adapters are available.
                /// </summary>
                ERROR_CLUSTER_NO_NET_ADAPTERS = 5906,
                /// <summary>
                /// The cluster node has been poisoned.
                /// </summary>
                ERROR_CLUSTER_POISONED = 5907,
                /// <summary>
                /// The group is unable to accept the request since it is moving to another node.
                /// </summary>
                ERROR_CLUSTER_GROUP_MOVING = 5908,
                /// <summary>
                /// The resource type cannot accept the request since is too busy performing another operation.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_TYPE_BUSY = 5909,
                /// <summary>
                /// The call to the cluster resource DLL timed out.
                /// </summary>
                ERROR_RESOURCE_CALL_TIMED_OUT = 5910,
                /// <summary>
                /// The address is not valid for an IPv6 Address resource. A global IPv6 address is required, and it must match a cluster network. Compatibility addresses are not permitted.
                /// </summary>
                ERROR_INVALID_CLUSTER_IPV6_ADDRESS = 5911,
                /// <summary>
                /// An internal cluster error occurred. A call to an invalid function was attempted.
                /// </summary>
                ERROR_CLUSTER_INTERNAL_INVALID_FUNCTION = 5912,
                /// <summary>
                /// A parameter value is out of acceptable range.
                /// </summary>
                ERROR_CLUSTER_PARAMETER_OUT_OF_BOUNDS = 5913,
                /// <summary>
                /// A network error occurred while sending data to another node in the cluster. The number of bytes transmitted was less than required.
                /// </summary>
                ERROR_CLUSTER_PARTIAL_SEND = 5914,
                /// <summary>
                /// An invalid cluster registry operation was attempted.
                /// </summary>
                ERROR_CLUSTER_REGISTRY_INVALID_FUNCTION = 5915,
                /// <summary>
                /// An input string of characters is not properly terminated.
                /// </summary>
                ERROR_CLUSTER_INVALID_STRING_TERMINATION = 5916,
                /// <summary>
                /// An input string of characters is not in a valid format for the data it represents.
                /// </summary>
                ERROR_CLUSTER_INVALID_STRING_FORMAT = 5917,
                /// <summary>
                /// An internal cluster error occurred. A cluster database transaction was attempted while a transaction was already in progress.
                /// </summary>
                ERROR_CLUSTER_DATABASE_TRANSACTION_IN_PROGRESS = 5918,
                /// <summary>
                /// An internal cluster error occurred. There was an attempt to commit a cluster database transaction while no transaction was in progress.
                /// </summary>
                ERROR_CLUSTER_DATABASE_TRANSACTION_NOT_IN_PROGRESS = 5919,
                /// <summary>
                /// An internal cluster error occurred. Data was not properly initialized.
                /// </summary>
                ERROR_CLUSTER_NULL_DATA = 5920,
                /// <summary>
                /// An error occurred while reading from a stream of data. An unexpected number of bytes was returned.
                /// </summary>
                ERROR_CLUSTER_PARTIAL_READ = 5921,
                /// <summary>
                /// An error occurred while writing to a stream of data. The required number of bytes could not be written.
                /// </summary>
                ERROR_CLUSTER_PARTIAL_WRITE = 5922,
                /// <summary>
                /// An error occurred while deserializing a stream of cluster data.
                /// </summary>
                ERROR_CLUSTER_CANT_DESERIALIZE_DATA = 5923,
                /// <summary>
                /// One or more property values for this resource are in conflict with one or more property values associated with its dependent resource(s).
                /// </summary>
                ERROR_DEPENDENT_RESOURCE_PROPERTY_CONFLICT = 5924,
                /// <summary>
                /// A quorum of cluster nodes was not present to form a cluster.
                /// </summary>
                ERROR_CLUSTER_NO_QUORUM = 5925,
                /// <summary>
                /// The cluster network is not valid for an IPv6 Address resource, or it does not match the configured address.
                /// </summary>
                ERROR_CLUSTER_INVALID_IPV6_NETWORK = 5926,
                /// <summary>
                /// The cluster network is not valid for an IPv6 Tunnel resource. Check the configuration of the IP Address resource on which the IPv6 Tunnel resource depends.
                /// </summary>
                ERROR_CLUSTER_INVALID_IPV6_TUNNEL_NETWORK = 5927,
                /// <summary>
                /// Quorum resource cannot reside in the Available Storage group.
                /// </summary>
                ERROR_QUORUM_NOT_ALLOWED_IN_THIS_GROUP = 5928,
                /// <summary>
                /// The dependencies for this resource are nested too deeply.
                /// </summary>
                ERROR_DEPENDENCY_TREE_TOO_COMPLEX = 5929,
                /// <summary>
                /// The call into the resource DLL raised an unhandled exception.
                /// </summary>
                ERROR_EXCEPTION_IN_RESOURCE_CALL = 5930,
                /// <summary>
                /// The RHS process failed to initialize.
                /// </summary>
                ERROR_CLUSTER_RHS_FAILED_INITIALIZATION = 5931,
                /// <summary>
                /// The Failover Clustering feature is not installed on this node.
                /// </summary>
                ERROR_CLUSTER_NOT_INSTALLED = 5932,
                /// <summary>
                /// The resources must be online on the same node for this operation.
                /// </summary>
                ERROR_CLUSTER_RESOURCES_MUST_BE_ONLINE_ON_THE_SAME_NODE = 5933,
                /// <summary>
                /// A new node can not be added since this cluster is already at its maximum number of nodes.
                /// </summary>
                ERROR_CLUSTER_MAX_NODES_IN_CLUSTER = 5934,
                /// <summary>
                /// This cluster can not be created since the specified number of nodes exceeds the maximum allowed limit.
                /// </summary>
                ERROR_CLUSTER_TOO_MANY_NODES = 5935,
                /// <summary>
                /// An attempt to use the specified cluster name failed because an enabled computer object with the given name already exists in the domain.
                /// </summary>
                ERROR_CLUSTER_OBJECT_ALREADY_USED = 5936,
                /// <summary>
                /// This cluster cannot be destroyed. It has non-core application groups which must be deleted before the cluster can be destroyed.
                /// </summary>
                ERROR_NONCORE_GROUPS_FOUND = 5937,
                /// <summary>
                /// File share associated with file share witness resource cannot be hosted by this cluster or any of its nodes.
                /// </summary>
                ERROR_FILE_SHARE_RESOURCE_CONFLICT = 5938,
                /// <summary>
                /// Eviction of this node is invalid at this time. Due to quorum requirements node eviction will result in cluster shutdown. If it is the last node in the cluster, destroy cluster command should be used.
                /// </summary>
                ERROR_CLUSTER_EVICT_INVALID_REQUEST = 5939,
                /// <summary>
                /// Only one instance of this resource type is allowed in the cluster.
                /// </summary>
                ERROR_CLUSTER_SINGLETON_RESOURCE = 5940,
                /// <summary>
                /// Only one instance of this resource type is allowed per resource group.
                /// </summary>
                ERROR_CLUSTER_GROUP_SINGLETON_RESOURCE = 5941,
                /// <summary>
                /// The resource failed to come online due to the failure of one or more provider resources.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_PROVIDER_FAILED = 5942,
                /// <summary>
                /// The resource has indicated that it cannot come online on any node.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_CONFIGURATION_ERROR = 5943,
                /// <summary>
                /// The current operation cannot be performed on this group at this time.
                /// </summary>
                ERROR_CLUSTER_GROUP_BUSY = 5944,
                /// <summary>
                /// The directory or file is not located on a cluster shared volume.
                /// </summary>
                ERROR_CLUSTER_NOT_SHARED_VOLUME = 5945,
                /// <summary>
                /// The Security Descriptor does not meet the requirements for a cluster.
                /// </summary>
                ERROR_CLUSTER_INVALID_SECURITY_DESCRIPTOR = 5946,
                /// <summary>
                /// There is one or more shared volumes resources configured in the cluster. Those resources must be moved to available storage in order for operation to succeed.
                /// </summary>
                ERROR_CLUSTER_SHARED_VOLUMES_IN_USE = 5947,
                /// <summary>
                /// This group or resource cannot be directly manipulated. Use shared volume APIs to perform desired operation.
                /// </summary>
                ERROR_CLUSTER_USE_SHARED_VOLUMES_API = 5948,
                /// <summary>
                /// Back up is in progress. Please wait for backup completion before trying this operation again.
                /// </summary>
                ERROR_CLUSTER_BACKUP_IN_PROGRESS = 5949,
                /// <summary>
                /// The path does not belong to a cluster shared volume.
                /// </summary>
                ERROR_NON_CSV_PATH = 5950,
                /// <summary>
                /// The cluster shared volume is not locally mounted on this node.
                /// </summary>
                ERROR_CSV_VOLUME_NOT_LOCAL = 5951,
                /// <summary>
                /// The cluster watchdog is terminating.
                /// </summary>
                ERROR_CLUSTER_WATCHDOG_TERMINATING = 5952,
                /// <summary>
                /// A resource vetoed a move between two nodes because they are incompatible.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_VETOED_MOVE_INCOMPATIBLE_NODES = 5953,
                /// <summary>
                /// The request is invalid either because node weight cannot be changed while the cluster is in disk-only quorum mode, or because changing the node weight would violate the minimum cluster quorum requirements.
                /// </summary>
                ERROR_CLUSTER_INVALID_NODE_WEIGHT = 5954,
                /// <summary>
                /// The resource vetoed the call.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_VETOED_CALL = 5955,
                /// <summary>
                /// Resource could not start or run because it could not reserve sufficient system resources.
                /// </summary>
                ERROR_RESMON_SYSTEM_RESOURCES_LACKING = 5956,
                /// <summary>
                /// A resource vetoed a move between two nodes because the destination currently does not have enough resources to complete the operation.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_VETOED_MOVE_NOT_ENOUGH_RESOURCES_ON_DESTINATION = 5957,
                /// <summary>
                /// A resource vetoed a move between two nodes because the source currently does not have enough resources to complete the operation.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_VETOED_MOVE_NOT_ENOUGH_RESOURCES_ON_SOURCE = 5958,
                /// <summary>
                /// The requested operation can not be completed because the group is queued for an operation.
                /// </summary>
                ERROR_CLUSTER_GROUP_QUEUED = 5959,
                /// <summary>
                /// The requested operation can not be completed because a resource has locked status.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_LOCKED_STATUS = 5960,
                /// <summary>
                /// The resource cannot move to another node because a cluster shared volume vetoed the operation.
                /// </summary>
                ERROR_CLUSTER_SHARED_VOLUME_FAILOVER_NOT_ALLOWED = 5961,
                /// <summary>
                /// A node drain is already in progress. This value was also named ERROR_CLUSTER_NODE_EVACUATION_IN_PROGRESS
                /// </summary>
                ERROR_CLUSTER_NODE_DRAIN_IN_PROGRESS = 5962,
                /// <summary>
                /// Clustered storage is not connected to the node.
                /// </summary>
                ERROR_CLUSTER_DISK_NOT_CONNECTED = 5963,
                /// <summary>
                /// The disk is not configured in a way to be used with CSV. CSV disks must have at least one partition that is formatted with NTFS.
                /// </summary>
                ERROR_DISK_NOT_CSV_CAPABLE = 5964,
                /// <summary>
                /// The resource must be part of the Available Storage group to complete this action.
                /// </summary>
                ERROR_RESOURCE_NOT_IN_AVAILABLE_STORAGE = 5965,
                /// <summary>
                /// CSVFS failed operation as volume is in redirected mode.
                /// </summary>
                ERROR_CLUSTER_SHARED_VOLUME_REDIRECTED = 5966,
                /// <summary>
                /// CSVFS failed operation as volume is not in redirected mode.
                /// </summary>
                ERROR_CLUSTER_SHARED_VOLUME_NOT_REDIRECTED = 5967,
                /// <summary>
                /// Cluster properties cannot be returned at this time.
                /// </summary>
                ERROR_CLUSTER_CANNOT_RETURN_PROPERTIES = 5968,
                /// <summary>
                /// The clustered disk resource contains software snapshot diff area that are not supported for Cluster Shared Volumes.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_CONTAINS_UNSUPPORTED_DIFF_AREA_FOR_SHARED_VOLUMES = 5969,
                /// <summary>
                /// The operation cannot be completed because the resource is in maintenance mode.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_IS_IN_MAINTENANCE_MODE = 5970,
                /// <summary>
                /// The operation cannot be completed because of cluster affinity conflicts.
                /// </summary>
                ERROR_CLUSTER_AFFINITY_CONFLICT = 5971,
                /// <summary>
                /// The operation cannot be completed because the resource is a replica virtual machine.
                /// </summary>
                ERROR_CLUSTER_RESOURCE_IS_REPLICA_VIRTUAL_MACHINE = 5972,
                /// <summary>
                /// The specified file could not be encrypted.
                /// </summary>
                ERROR_ENCRYPTION_FAILED = 6000,
                /// <summary>
                /// The specified file could not be decrypted.
                /// </summary>
                ERROR_DECRYPTION_FAILED = 6001,
                /// <summary>
                /// The specified file is encrypted and the user does not have the ability to decrypt it.
                /// </summary>
                ERROR_FILE_ENCRYPTED = 6002,
                /// <summary>
                /// There is no valid encryption recovery policy configured for this system.
                /// </summary>
                ERROR_NO_RECOVERY_POLICY = 6003,
                /// <summary>
                /// The required encryption driver is not loaded for this system.
                /// </summary>
                ERROR_NO_EFS = 6004,
                /// <summary>
                /// The file was encrypted with a different encryption driver than is currently loaded.
                /// </summary>
                ERROR_WRONG_EFS = 6005,
                /// <summary>
                /// There are no EFS keys defined for the user.
                /// </summary>
                ERROR_NO_USER_KEYS = 6006,
                /// <summary>
                /// The specified file is not encrypted.
                /// </summary>
                ERROR_FILE_NOT_ENCRYPTED = 6007,
                /// <summary>
                /// The specified file is not in the defined EFS export format.
                /// </summary>
                ERROR_NOT_EXPORT_FORMAT = 6008,
                /// <summary>
                /// The specified file is read only.
                /// </summary>
                ERROR_FILE_READ_ONLY = 6009,
                /// <summary>
                /// The directory has been disabled for encryption.
                /// </summary>
                ERROR_DIR_EFS_DISALLOWED = 6010,
                /// <summary>
                /// The server is not trusted for remote encryption operation.
                /// </summary>
                ERROR_EFS_SERVER_NOT_TRUSTED = 6011,
                /// <summary>
                /// Recovery policy configured for this system contains invalid recovery certificate.
                /// </summary>
                ERROR_BAD_RECOVERY_POLICY = 6012,
                /// <summary>
                /// The encryption algorithm used on the source file needs a bigger key buffer than the one on the destination file.
                /// </summary>
                ERROR_EFS_ALG_BLOB_TOO_BIG = 6013,
                /// <summary>
                /// The disk partition does not support file encryption.
                /// </summary>
                ERROR_VOLUME_NOT_SUPPORT_EFS = 6014,
                /// <summary>
                /// This machine is disabled for file encryption.
                /// </summary>
                ERROR_EFS_DISABLED = 6015,
                /// <summary>
                /// A newer system is required to decrypt this encrypted file.
                /// </summary>
                ERROR_EFS_VERSION_NOT_SUPPORT = 6016,
                /// <summary>
                /// The remote server sent an invalid response for a file being opened with Client Side Encryption.
                /// </summary>
                ERROR_CS_ENCRYPTION_INVALID_SERVER_RESPONSE = 6017,
                /// <summary>
                /// Client Side Encryption is not supported by the remote server even though it claims to support it.
                /// </summary>
                ERROR_CS_ENCRYPTION_UNSUPPORTED_SERVER = 6018,
                /// <summary>
                /// File is encrypted and should be opened in Client Side Encryption mode.
                /// </summary>
                ERROR_CS_ENCRYPTION_EXISTING_ENCRYPTED_FILE = 6019,
                /// <summary>
                /// A new encrypted file is being created and a $EFS needs to be provided.
                /// </summary>
                ERROR_CS_ENCRYPTION_NEW_ENCRYPTED_FILE = 6020,
                /// <summary>
                /// The SMB client requested a CSE FSCTL on a non-CSE file.
                /// </summary>
                ERROR_CS_ENCRYPTION_FILE_NOT_CSE = 6021,
                /// <summary>
                /// The requested operation was blocked by policy. For more information, contact your system administrator.
                /// </summary>
                ERROR_ENCRYPTION_POLICY_DENIES_OPERATION = 6022,
                /// <summary>
                /// The list of servers for this workgroup is not currently available.
                /// </summary>
                ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
                /// <summary>
                /// The Task Scheduler service must be configured to run in the System account to function properly. Individual tasks may be configured to run in other accounts.
                /// </summary>
                SCHED_E_SERVICE_NOT_LOCALSYSTEM = 6200,
                /// <summary>
                /// Log service encountered an invalid log sector.
                /// </summary>
                ERROR_LOG_SECTOR_INVALID = 6600,
                /// <summary>
                /// Log service encountered a log sector with invalid block parity.
                /// </summary>
                ERROR_LOG_SECTOR_PARITY_INVALID = 6601,
                /// <summary>
                /// Log service encountered a remapped log sector.
                /// </summary>
                ERROR_LOG_SECTOR_REMAPPED = 6602,
                /// <summary>
                /// Log service encountered a partial or incomplete log block.
                /// </summary>
                ERROR_LOG_BLOCK_INCOMPLETE = 6603,
                /// <summary>
                /// Log service encountered an attempt access data outside the active log range.
                /// </summary>
                ERROR_LOG_INVALID_RANGE = 6604,
                /// <summary>
                /// Log service user marshalling buffers are exhausted.
                /// </summary>
                ERROR_LOG_BLOCKS_EXHAUSTED = 6605,
                /// <summary>
                /// Log service encountered an attempt read from a marshalling area with an invalid read context.
                /// </summary>
                ERROR_LOG_READ_CONTEXT_INVALID = 6606,
                /// <summary>
                /// Log service encountered an invalid log restart area.
                /// </summary>
                ERROR_LOG_RESTART_INVALID = 6607,
                /// <summary>
                /// Log service encountered an invalid log block version.
                /// </summary>
                ERROR_LOG_BLOCK_VERSION = 6608,
                /// <summary>
                /// Log service encountered an invalid log block.
                /// </summary>
                ERROR_LOG_BLOCK_INVALID = 6609,
                /// <summary>
                /// Log service encountered an attempt to read the log with an invalid read mode.
                /// </summary>
                ERROR_LOG_READ_MODE_INVALID = 6610,
                /// <summary>
                /// Log service encountered a log stream with no restart area.
                /// </summary>
                ERROR_LOG_NO_RESTART = 6611,
                /// <summary>
                /// Log service encountered a corrupted metadata file.
                /// </summary>
                ERROR_LOG_METADATA_CORRUPT = 6612,
                /// <summary>
                /// Log service encountered a metadata file that could not be created by the log file system.
                /// </summary>
                ERROR_LOG_METADATA_INVALID = 6613,
                /// <summary>
                /// Log service encountered a metadata file with inconsistent data.
                /// </summary>
                ERROR_LOG_METADATA_INCONSISTENT = 6614,
                /// <summary>
                /// Log service encountered an attempt to erroneous allocate or dispose reservation space.
                /// </summary>
                ERROR_LOG_RESERVATION_INVALID = 6615,
                /// <summary>
                /// Log service cannot delete log file or file system container.
                /// </summary>
                ERROR_LOG_CANT_DELETE = 6616,
                /// <summary>
                /// Log service has reached the maximum allowable containers allocated to a log file.
                /// </summary>
                ERROR_LOG_CONTAINER_LIMIT_EXCEEDED = 6617,
                /// <summary>
                /// Log service has attempted to read or write backward past the start of the log.
                /// </summary>
                ERROR_LOG_START_OF_LOG = 6618,
                /// <summary>
                /// Log policy could not be installed because a policy of the same type is already present.
                /// </summary>
                ERROR_LOG_POLICY_ALREADY_INSTALLED = 6619,
                /// <summary>
                /// Log policy in question was not installed at the time of the request.
                /// </summary>
                ERROR_LOG_POLICY_NOT_INSTALLED = 6620,
                /// <summary>
                /// The installed set of policies on the log is invalid.
                /// </summary>
                ERROR_LOG_POLICY_INVALID = 6621,
                /// <summary>
                /// A policy on the log in question prevented the operation from completing.
                /// </summary>
                ERROR_LOG_POLICY_CONFLICT = 6622,
                /// <summary>
                /// Log space cannot be reclaimed because the log is pinned by the archive tail.
                /// </summary>
                ERROR_LOG_PINNED_ARCHIVE_TAIL = 6623,
                /// <summary>
                /// Log record is not a record in the log file.
                /// </summary>
                ERROR_LOG_RECORD_NONEXISTENT = 6624,
                /// <summary>
                /// Number of reserved log records or the adjustment of the number of reserved log records is invalid.
                /// </summary>
                ERROR_LOG_RECORDS_RESERVED_INVALID = 6625,
                /// <summary>
                /// Reserved log space or the adjustment of the log space is invalid.
                /// </summary>
                ERROR_LOG_SPACE_RESERVED_INVALID = 6626,
                /// <summary>
                /// An new or existing archive tail or base of the active log is invalid.
                /// </summary>
                ERROR_LOG_TAIL_INVALID = 6627,
                /// <summary>
                /// Log space is exhausted.
                /// </summary>
                ERROR_LOG_FULL = 6628,
                /// <summary>
                /// The log could not be set to the requested size.
                /// </summary>
                ERROR_COULD_NOT_RESIZE_LOG = 6629,
                /// <summary>
                /// Log is multiplexed, no direct writes to the physical log is allowed.
                /// </summary>
                ERROR_LOG_MULTIPLEXED = 6630,
                /// <summary>
                /// The operation failed because the log is a dedicated log.
                /// </summary>
                ERROR_LOG_DEDICATED = 6631,
                /// <summary>
                /// The operation requires an archive context.
                /// </summary>
                ERROR_LOG_ARCHIVE_NOT_IN_PROGRESS = 6632,
                /// <summary>
                /// Log archival is in progress.
                /// </summary>
                ERROR_LOG_ARCHIVE_IN_PROGRESS = 6633,
                /// <summary>
                /// The operation requires a non-ephemeral log, but the log is ephemeral.
                /// </summary>
                ERROR_LOG_EPHEMERAL = 6634,
                /// <summary>
                /// The log must have at least two containers before it can be read from or written to.
                /// </summary>
                ERROR_LOG_NOT_ENOUGH_CONTAINERS = 6635,
                /// <summary>
                /// A log client has already registered on the stream.
                /// </summary>
                ERROR_LOG_CLIENT_ALREADY_REGISTERED = 6636,
                /// <summary>
                /// A log client has not been registered on the stream.
                /// </summary>
                ERROR_LOG_CLIENT_NOT_REGISTERED = 6637,
                /// <summary>
                /// A request has already been made to handle the log full condition.
                /// </summary>
                ERROR_LOG_FULL_HANDLER_IN_PROGRESS = 6638,
                /// <summary>
                /// Log service encountered an error when attempting to read from a log container.
                /// </summary>
                ERROR_LOG_CONTAINER_READ_FAILED = 6639,
                /// <summary>
                /// Log service encountered an error when attempting to write to a log container.
                /// </summary>
                ERROR_LOG_CONTAINER_WRITE_FAILED = 6640,
                /// <summary>
                /// Log service encountered an error when attempting open a log container.
                /// </summary>
                ERROR_LOG_CONTAINER_OPEN_FAILED = 6641,
                /// <summary>
                /// Log service encountered an invalid container state when attempting a requested action.
                /// </summary>
                ERROR_LOG_CONTAINER_STATE_INVALID = 6642,
                /// <summary>
                /// Log service is not in the correct state to perform a requested action.
                /// </summary>
                ERROR_LOG_STATE_INVALID = 6643,
                /// <summary>
                /// Log space cannot be reclaimed because the log is pinned.
                /// </summary>
                ERROR_LOG_PINNED = 6644,
                /// <summary>
                /// Log metadata flush failed.
                /// </summary>
                ERROR_LOG_METADATA_FLUSH_FAILED = 6645,
                /// <summary>
                /// Security on the log and its containers is inconsistent.
                /// </summary>
                ERROR_LOG_INCONSISTENT_SECURITY = 6646,
                /// <summary>
                /// Records were appended to the log or reservation changes were made, but the log could not be flushed.
                /// </summary>
                ERROR_LOG_APPENDED_FLUSH_FAILED = 6647,
                /// <summary>
                /// The log is pinned due to reservation consuming most of the log space. Free some reserved records to make space available.
                /// </summary>
                ERROR_LOG_PINNED_RESERVATION = 6648,
                /// <summary>
                /// The transaction handle associated with this operation is not valid.
                /// </summary>
                ERROR_INVALID_TRANSACTION = 6700,
                /// <summary>
                /// The requested operation was made in the context of a transaction that is no longer active.
                /// </summary>
                ERROR_TRANSACTION_NOT_ACTIVE = 6701,
                /// <summary>
                /// The requested operation is not valid on the Transaction object in its current state.
                /// </summary>
                ERROR_TRANSACTION_REQUEST_NOT_VALID = 6702,
                /// <summary>
                /// The caller has called a response API, but the response is not expected because the TM did not issue the corresponding request to the caller.
                /// </summary>
                ERROR_TRANSACTION_NOT_REQUESTED = 6703,
                /// <summary>
                /// It is too late to perform the requested operation, since the Transaction has already been aborted.
                /// </summary>
                ERROR_TRANSACTION_ALREADY_ABORTED = 6704,
                /// <summary>
                /// It is too late to perform the requested operation, since the Transaction has already been committed.
                /// </summary>
                ERROR_TRANSACTION_ALREADY_COMMITTED = 6705,
                /// <summary>
                /// The Transaction Manager was unable to be successfully initialized. Transacted operations are not supported.
                /// </summary>
                ERROR_TM_INITIALIZATION_FAILED = 6706,
                /// <summary>
                /// The specified ResourceManager made no changes or updates to the resource under this transaction.
                /// </summary>
                ERROR_RESOURCEMANAGER_READ_ONLY = 6707,
                /// <summary>
                /// The resource manager has attempted to prepare a transaction that it has not successfully joined.
                /// </summary>
                ERROR_TRANSACTION_NOT_JOINED = 6708,
                /// <summary>
                /// The Transaction object already has a superior enlistment, and the caller attempted an operation that would have created a new superior. Only a single superior enlistment is allow.
                /// </summary>
                ERROR_TRANSACTION_SUPERIOR_EXISTS = 6709,
                /// <summary>
                /// The RM tried to register a protocol that already exists.
                /// </summary>
                ERROR_CRM_PROTOCOL_ALREADY_EXISTS = 6710,
                /// <summary>
                /// The attempt to propagate the Transaction failed.
                /// </summary>
                ERROR_TRANSACTION_PROPAGATION_FAILED = 6711,
                /// <summary>
                /// The requested propagation protocol was not registered as a CRM.
                /// </summary>
                ERROR_CRM_PROTOCOL_NOT_FOUND = 6712,
                /// <summary>
                /// The buffer passed in to PushTransaction or PullTransaction is not in a valid format.
                /// </summary>
                ERROR_TRANSACTION_INVALID_MARSHALL_BUFFER = 6713,
                /// <summary>
                /// The current transaction context associated with the thread is not a valid handle to a transaction object.
                /// </summary>
                ERROR_CURRENT_TRANSACTION_NOT_VALID = 6714,
                /// <summary>
                /// The specified Transaction object could not be opened, because it was not found.
                /// </summary>
                ERROR_TRANSACTION_NOT_FOUND = 6715,
                /// <summary>
                /// The specified ResourceManager object could not be opened, because it was not found.
                /// </summary>
                ERROR_RESOURCEMANAGER_NOT_FOUND = 6716,
                /// <summary>
                /// The specified Enlistment object could not be opened, because it was not found.
                /// </summary>
                ERROR_ENLISTMENT_NOT_FOUND = 6717,
                /// <summary>
                /// The specified TransactionManager object could not be opened, because it was not found.
                /// </summary>
                ERROR_TRANSACTIONMANAGER_NOT_FOUND = 6718,
                /// <summary>
                /// The object specified could not be created or opened, because its associated TransactionManager is not online. The TransactionManager must be brought fully Online by calling RecoverTransactionManager to recover to the end of its LogFile before objects in its Transaction or ResourceManager namespaces can be opened.  In addition, errors in writing records to its LogFile can cause a TransactionManager to go offline.
                /// </summary>
                ERROR_TRANSACTIONMANAGER_NOT_ONLINE = 6719,
                /// <summary>
                /// The specified TransactionManager was unable to create the objects contained in its logfile in the Ob namespace. Therefore, the TransactionManager was unable to recover.
                /// </summary>
                ERROR_TRANSACTIONMANAGER_RECOVERY_NAME_COLLISION = 6720,
                /// <summary>
                /// The call to create a superior Enlistment on this Transaction object could not be completed, because the Transaction object specified for the enlistment is a subordinate branch of the Transaction. Only the root of the Transaction can be enlisted on as a superior.
                /// </summary>
                ERROR_TRANSACTION_NOT_ROOT = 6721,
                /// <summary>
                /// Because the associated transaction manager or resource manager has been closed, the handle is no longer valid.
                /// </summary>
                ERROR_TRANSACTION_OBJECT_EXPIRED = 6722,
                /// <summary>
                /// The specified operation could not be performed on this Superior enlistment, because the enlistment was not created with the corresponding completion response in the NotificationMask.
                /// </summary>
                ERROR_TRANSACTION_RESPONSE_NOT_ENLISTED = 6723,
                /// <summary>
                /// The specified operation could not be performed, because the record that would be logged was too long. This can occur because of two conditions: either there are too many Enlistments on this Transaction, or the combined RecoveryInformation being logged on behalf of those Enlistments is too long.
                /// </summary>
                ERROR_TRANSACTION_RECORD_TOO_LONG = 6724,
                /// <summary>
                /// Implicit transaction are not supported.
                /// </summary>
                ERROR_IMPLICIT_TRANSACTION_NOT_SUPPORTED = 6725,
                /// <summary>
                /// The kernel transaction manager had to abort or forget the transaction because it blocked forward progress.
                /// </summary>
                ERROR_TRANSACTION_INTEGRITY_VIOLATED = 6726,
                /// <summary>
                /// The TransactionManager identity that was supplied did not match the one recorded in the TransactionManager's log file.
                /// </summary>
                ERROR_TRANSACTIONMANAGER_IDENTITY_MISMATCH = 6727,
                /// <summary>
                /// This snapshot operation cannot continue because a transactional resource manager cannot be frozen in its current state.  Please try again.
                /// </summary>
                ERROR_RM_CANNOT_BE_FROZEN_FOR_SNAPSHOT = 6728,
                /// <summary>
                /// The transaction cannot be enlisted on with the specified EnlistmentMask, because the transaction has already completed the PrePrepare phase.  In order to ensure correctness, the ResourceManager must switch to a write-      through mode and cease caching data within this transaction.  Enlisting for only subsequent transaction phases may still succeed.
                /// </summary>
                ERROR_TRANSACTION_MUST_WRITETHROUGH = 6729,
                /// <summary>
                /// The transaction does not have a superior enlistment.
                /// </summary>
                ERROR_TRANSACTION_NO_SUPERIOR = 6730,
                /// <summary>
                /// The attempt to commit the Transaction completed, but it is possible that some portion of the transaction tree did not commit successfully due to heuristics.  Therefore it is possible that some data modified in the transaction may not have committed, resulting in transactional inconsistency.  If possible, check the consistency of the associated data.
                /// </summary>
                ERROR_HEURISTIC_DAMAGE_POSSIBLE = 6731,
                /// <summary>
                /// The function attempted to use a name that is reserved for use by another transaction.
                /// </summary>
                ERROR_TRANSACTIONAL_CONFLICT = 6800,
                /// <summary>
                /// Transaction support within the specified resource manager is not started or was shut down due to an error.
                /// </summary>
                ERROR_RM_NOT_ACTIVE = 6801,
                /// <summary>
                /// The metadata of the RM has been corrupted. The RM will not function.
                /// </summary>
                ERROR_RM_METADATA_CORRUPT = 6802,
                /// <summary>
                /// The specified directory does not contain a resource manager.
                /// </summary>
                ERROR_DIRECTORY_NOT_RM = 6803,
                /// <summary>
                /// The remote server or share does not support transacted file operations.
                /// </summary>
                ERROR_TRANSACTIONS_UNSUPPORTED_REMOTE = 6805,
                /// <summary>
                /// The requested log size is invalid.
                /// </summary>
                ERROR_LOG_RESIZE_INVALID_SIZE = 6806,
                /// <summary>
                /// The object (file, stream, link) corresponding to the handle has been deleted by a Transaction Savepoint Rollback.
                /// </summary>
                ERROR_OBJECT_NO_LONGER_EXISTS = 6807,
                /// <summary>
                /// The specified file miniversion was not found for this transacted file open.
                /// </summary>
                ERROR_STREAM_MINIVERSION_NOT_FOUND = 6808,
                /// <summary>
                /// The specified file miniversion was found but has been invalidated. Most likely cause is a transaction savepoint rollback.
                /// </summary>
                ERROR_STREAM_MINIVERSION_NOT_VALID = 6809,
                /// <summary>
                /// A miniversion may only be opened in the context of the transaction that created it.
                /// </summary>
                ERROR_MINIVERSION_INACCESSIBLE_FROM_SPECIFIED_TRANSACTION = 6810,
                /// <summary>
                /// It is not possible to open a miniversion with modify access.
                /// </summary>
                ERROR_CANT_OPEN_MINIVERSION_WITH_MODIFY_INTENT = 6811,
                /// <summary>
                /// It is not possible to create any more miniversions for this stream.
                /// </summary>
                ERROR_CANT_CREATE_MORE_STREAM_MINIVERSIONS = 6812,
                /// <summary>
                /// The remote server sent mismatching version number or Fid for a file opened with transactions.
                /// </summary>
                ERROR_REMOTE_FILE_VERSION_MISMATCH = 6814,
                /// <summary>
                /// The handle has been invalidated by a transaction. The most likely cause is the presence of memory mapping on a file or an open handle when the transaction ended or rolled back to savepoint.
                /// </summary>
                ERROR_HANDLE_NO_LONGER_VALID = 6815,
                /// <summary>
                /// There is no transaction metadata on the file.
                /// </summary>
                ERROR_NO_TXF_METADATA = 6816,
                /// <summary>
                /// The log data is corrupt.
                /// </summary>
                ERROR_LOG_CORRUPTION_DETECTED = 6817,
                /// <summary>
                /// The file can't be recovered because there is a handle still open on it.
                /// </summary>
                ERROR_CANT_RECOVER_WITH_HANDLE_OPEN = 6818,
                /// <summary>
                /// The transaction outcome is unavailable because the resource manager responsible for it has disconnected.
                /// </summary>
                ERROR_RM_DISCONNECTED = 6819,
                /// <summary>
                /// The request was rejected because the enlistment in question is not a superior enlistment.
                /// </summary>
                ERROR_ENLISTMENT_NOT_SUPERIOR = 6820,
                /// <summary>
                /// The transactional resource manager is already consistent. Recovery is not needed.
                /// </summary>
                ERROR_RECOVERY_NOT_NEEDED = 6821,
                /// <summary>
                /// The transactional resource manager has already been started.
                /// </summary>
                ERROR_RM_ALREADY_STARTED = 6822,
                /// <summary>
                /// The file cannot be opened transactionally, because its identity depends on the outcome of an unresolved transaction.
                /// </summary>
                ERROR_FILE_IDENTITY_NOT_PERSISTENT = 6823,
                /// <summary>
                /// The operation cannot be performed because another transaction is depending on the fact that this property will not change.
                /// </summary>
                ERROR_CANT_BREAK_TRANSACTIONAL_DEPENDENCY = 6824,
                /// <summary>
                /// The operation would involve a single file with two transactional resource managers and is therefore not allowed.
                /// </summary>
                ERROR_CANT_CROSS_RM_BOUNDARY = 6825,
                /// <summary>
                /// The $Txf directory must be empty for this operation to succeed.
                /// </summary>
                ERROR_TXF_DIR_NOT_EMPTY = 6826,
                /// <summary>
                /// The operation would leave a transactional resource manager in an inconsistent state and is therefore not allowed.
                /// </summary>
                ERROR_INDOUBT_TRANSACTIONS_EXIST = 6827,
                /// <summary>
                /// The operation could not be completed because the transaction manager does not have a log.
                /// </summary>
                ERROR_TM_VOLATILE = 6828,
                /// <summary>
                /// A rollback could not be scheduled because a previously scheduled rollback has already executed or been queued for execution.
                /// </summary>
                ERROR_ROLLBACK_TIMER_EXPIRED = 6829,
                /// <summary>
                /// The transactional metadata attribute on the file or directory is corrupt and unreadable.
                /// </summary>
                ERROR_TXF_ATTRIBUTE_CORRUPT = 6830,
                /// <summary>
                /// The encryption operation could not be completed because a transaction is active.
                /// </summary>
                ERROR_EFS_NOT_ALLOWED_IN_TRANSACTION = 6831,
                /// <summary>
                /// This object is not allowed to be opened in a transaction.
                /// </summary>
                ERROR_TRANSACTIONAL_OPEN_NOT_ALLOWED = 6832,
                /// <summary>
                /// An attempt to create space in the transactional resource manager's log failed. The failure status has been recorded in the event log.
                /// </summary>
                ERROR_LOG_GROWTH_FAILED = 6833,
                /// <summary>
                /// Memory mapping (creating a mapped section) a remote file under a transaction is not supported.
                /// </summary>
                ERROR_TRANSACTED_MAPPING_UNSUPPORTED_REMOTE = 6834,
                /// <summary>
                /// Transaction metadata is already present on this file and cannot be superseded.
                /// </summary>
                ERROR_TXF_METADATA_ALREADY_PRESENT = 6835,
                /// <summary>
                /// A transaction scope could not be entered because the scope handler has not been initialized.
                /// </summary>
                ERROR_TRANSACTION_SCOPE_CALLBACKS_NOT_SET = 6836,
                /// <summary>
                /// Promotion was required in order to allow the resource manager to enlist, but the transaction was set to disallow it.
                /// </summary>
                ERROR_TRANSACTION_REQUIRED_PROMOTION = 6837,
                /// <summary>
                /// This file is open for modification in an unresolved transaction and may be opened for execute only by a transacted reader.
                /// </summary>
                ERROR_CANNOT_EXECUTE_FILE_IN_TRANSACTION = 6838,
                /// <summary>
                /// The request to thaw frozen transactions was ignored because transactions had not previously been frozen.
                /// </summary>
                ERROR_TRANSACTIONS_NOT_FROZEN = 6839,
                /// <summary>
                /// Transactions cannot be frozen because a freeze is already in progress.
                /// </summary>
                ERROR_TRANSACTION_FREEZE_IN_PROGRESS = 6840,
                /// <summary>
                /// The target volume is not a snapshot volume. This operation is only valid on a volume mounted as a snapshot.
                /// </summary>
                ERROR_NOT_SNAPSHOT_VOLUME = 6841,
                /// <summary>
                /// The savepoint operation failed because files are open on the transaction. This is not permitted.
                /// </summary>
                ERROR_NO_SAVEPOINT_WITH_OPEN_FILES = 6842,
                /// <summary>
                /// Windows has discovered corruption in a file, and that file has since been repaired. Data loss may have occurred.
                /// </summary>
                ERROR_DATA_LOST_REPAIR = 6843,
                /// <summary>
                /// The sparse operation could not be completed because a transaction is active on the file.
                /// </summary>
                ERROR_SPARSE_NOT_ALLOWED_IN_TRANSACTION = 6844,
                /// <summary>
                /// The call to create a TransactionManager object failed because the Tm Identity stored in the logfile does not match the Tm Identity that was passed in as an argument.
                /// </summary>
                ERROR_TM_IDENTITY_MISMATCH = 6845,
                /// <summary>
                /// I/O was attempted on a section object that has been floated as a result of a transaction ending. There is no valid data.
                /// </summary>
                ERROR_FLOATED_SECTION = 6846,
                /// <summary>
                /// The transactional resource manager cannot currently accept transacted work due to a transient condition such as low resources.
                /// </summary>
                ERROR_CANNOT_ACCEPT_TRANSACTED_WORK = 6847,
                /// <summary>
                /// The transactional resource manager had too many tranactions outstanding that could not be aborted. The transactional resource manger has been shut down.
                /// </summary>
                ERROR_CANNOT_ABORT_TRANSACTIONS = 6848,
                /// <summary>
                /// The operation could not be completed due to bad clusters on disk.
                /// </summary>
                ERROR_BAD_CLUSTERS = 6849,
                /// <summary>
                /// The compression operation could not be completed because a transaction is active on the file.
                /// </summary>
                ERROR_COMPRESSION_NOT_ALLOWED_IN_TRANSACTION = 6850,
                /// <summary>
                /// The operation could not be completed because the volume is dirty. Please run chkdsk and try again.
                /// </summary>
                ERROR_VOLUME_DIRTY = 6851,
                /// <summary>
                /// The link tracking operation could not be completed because a transaction is active.
                /// </summary>
                ERROR_NO_LINK_TRACKING_IN_TRANSACTION = 6852,
                /// <summary>
                /// This operation cannot be performed in a transaction.
                /// </summary>
                ERROR_OPERATION_NOT_SUPPORTED_IN_TRANSACTION = 6853,
                /// <summary>
                /// The handle is no longer properly associated with its transaction.  It may have been opened in a transactional resource manager that was subsequently forced to restart.  Please close the handle and open a new one.
                /// </summary>
                ERROR_EXPIRED_HANDLE = 6854,
                /// <summary>
                /// The specified operation could not be performed because the resource manager is not enlisted in the transaction.
                /// </summary>
                ERROR_TRANSACTION_NOT_ENLISTED = 6855,
                /// <summary>
                /// The specified session name is invalid.
                /// </summary>
                ERROR_CTX_WINSTATION_NAME_INVALID = 7001,
                /// <summary>
                /// The specified protocol driver is invalid.
                /// </summary>
                ERROR_CTX_INVALID_PD = 7002,
                /// <summary>
                /// The specified protocol driver was not found in the system path.
                /// </summary>
                ERROR_CTX_PD_NOT_FOUND = 7003,
                /// <summary>
                /// The specified terminal connection driver was not found in the system path.
                /// </summary>
                ERROR_CTX_WD_NOT_FOUND = 7004,
                /// <summary>
                /// A registry key for event logging could not be created for this session.
                /// </summary>
                ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY = 7005,
                /// <summary>
                /// A service with the same name already exists on the system.
                /// </summary>
                ERROR_CTX_SERVICE_NAME_COLLISION = 7006,
                /// <summary>
                /// A close operation is pending on the session.
                /// </summary>
                ERROR_CTX_CLOSE_PENDING = 7007,
                /// <summary>
                /// There are no free output buffers available.
                /// </summary>
                ERROR_CTX_NO_OUTBUF = 7008,
                /// <summary>
                /// The MODEM.INF file was not found.
                /// </summary>
                ERROR_CTX_MODEM_INF_NOT_FOUND = 7009,
                /// <summary>
                /// The modem name was not found in MODEM.INF.
                /// </summary>
                ERROR_CTX_INVALID_MODEMNAME = 7010,
                /// <summary>
                /// The modem did not accept the command sent to it. Verify that the configured modem name matches the attached modem.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_ERROR = 7011,
                /// <summary>
                /// The modem did not respond to the command sent to it. Verify that the modem is properly cabled and powered on.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_TIMEOUT = 7012,
                /// <summary>
                /// Carrier detect has failed or carrier has been dropped due to disconnect.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_NO_CARRIER = 7013,
                /// <summary>
                /// Dial tone not detected within the required time. Verify that the phone cable is properly attached and functional.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE = 7014,
                /// <summary>
                /// Busy signal detected at remote site on callback.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_BUSY = 7015,
                /// <summary>
                /// Voice detected at remote site on callback.
                /// </summary>
                ERROR_CTX_MODEM_RESPONSE_VOICE = 7016,
                /// <summary>
                /// Transport driver error.
                /// </summary>
                ERROR_CTX_TD_ERROR = 7017,
                /// <summary>
                /// The specified session cannot be found.
                /// </summary>
                ERROR_CTX_WINSTATION_NOT_FOUND = 7022,
                /// <summary>
                /// The specified session name is already in use.
                /// </summary>
                ERROR_CTX_WINSTATION_ALREADY_EXISTS = 7023,
                /// <summary>
                /// The task you are trying to do can't be completed because Remote Desktop Services is currently busy. Please try again in a few minutes. Other users should still be able to log on.
                /// </summary>
                ERROR_CTX_WINSTATION_BUSY = 7024,
                /// <summary>
                /// An attempt has been made to connect to a session whose video mode is not supported by the current client.
                /// </summary>
                ERROR_CTX_BAD_VIDEO_MODE = 7025,
                /// <summary>
                /// The application attempted to enable DOS graphics mode. DOS graphics mode is not supported.
                /// </summary>
                ERROR_CTX_GRAPHICS_INVALID = 7035,
                /// <summary>
                /// Your interactive logon privilege has been disabled. Please contact your administrator.
                /// </summary>
                ERROR_CTX_LOGON_DISABLED = 7037,
                /// <summary>
                /// The requested operation can be performed only on the system console. This is most often the result of a driver or system DLL requiring direct console access.
                /// </summary>
                ERROR_CTX_NOT_CONSOLE = 7038,
                /// <summary>
                /// The client failed to respond to the server connect message.
                /// </summary>
                ERROR_CTX_CLIENT_QUERY_TIMEOUT = 7040,
                /// <summary>
                /// Disconnecting the console session is not supported.
                /// </summary>
                ERROR_CTX_CONSOLE_DISCONNECT = 7041,
                /// <summary>
                /// Reconnecting a disconnected session to the console is not supported.
                /// </summary>
                ERROR_CTX_CONSOLE_CONNECT = 7042,
                /// <summary>
                /// The request to control another session remotely was denied.
                /// </summary>
                ERROR_CTX_SHADOW_DENIED = 7044,
                /// <summary>
                /// The requested session access is denied.
                /// </summary>
                ERROR_CTX_WINSTATION_ACCESS_DENIED = 7045,
                /// <summary>
                /// The specified terminal connection driver is invalid.
                /// </summary>
                ERROR_CTX_INVALID_WD = 7049,
                /// <summary>
                /// The requested session cannot be controlled remotely. This may be because the session is disconnected or does not currently have a user logged on.
                /// </summary>
                ERROR_CTX_SHADOW_INVALID = 7050,
                /// <summary>
                /// The requested session is not configured to allow remote control.
                /// </summary>
                ERROR_CTX_SHADOW_DISABLED = 7051,
                /// <summary>
                /// Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number is currently being used by another user. Please call your system administrator to obtain a unique license number.
                /// </summary>
                ERROR_CTX_CLIENT_LICENSE_IN_USE = 7052,
                /// <summary>
                /// Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number has not been entered for this copy of the Terminal Server client. Please contact your system administrator.
                /// </summary>
                ERROR_CTX_CLIENT_LICENSE_NOT_SET = 7053,
                /// <summary>
                /// The number of connections to this computer is limited and all connections are in use right now. Try connecting later or contact your system administrator.
                /// </summary>
                ERROR_CTX_LICENSE_NOT_AVAILABLE = 7054,
                /// <summary>
                /// The client you are using is not licensed to use this system. Your logon request is denied.
                /// </summary>
                ERROR_CTX_LICENSE_CLIENT_INVALID = 7055,
                /// <summary>
                /// The system license has expired. Your logon request is denied.
                /// </summary>
                ERROR_CTX_LICENSE_EXPIRED = 7056,
                /// <summary>
                /// Remote control could not be terminated because the specified session is not currently being remotely controlled.
                /// </summary>
                ERROR_CTX_SHADOW_NOT_RUNNING = 7057,
                /// <summary>
                /// The remote control of the console was terminated because the display mode was changed. Changing the display mode in a remote control session is not supported.
                /// </summary>
                ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE = 7058,
                /// <summary>
                /// Activation has already been reset the maximum number of times for this installation. Your activation timer will not be cleared.
                /// </summary>
                ERROR_ACTIVATION_COUNT_EXCEEDED = 7059,
                /// <summary>
                /// Remote logins are currently disabled.
                /// </summary>
                ERROR_CTX_WINSTATIONS_DISABLED = 7060,
                /// <summary>
                /// You do not have the proper encryption level to access this Session.
                /// </summary>
                ERROR_CTX_ENCRYPTION_LEVEL_REQUIRED = 7061,
                /// <summary>
                /// The user %s\\\\%s is currently logged on to this computer. Only the current user or an administrator can log on to this computer.
                /// </summary>
                ERROR_CTX_SESSION_IN_USE = 7062,
                /// <summary>
                /// The user %s\\\\%s is already logged on to the console of this computer. You do not have permission to log in at this time. To resolve this issue, contact %s\\\\%s and have them log off.
                /// </summary>
                ERROR_CTX_NO_FORCE_LOGOFF = 7063,
                /// <summary>
                /// Unable to log you on because of an account restriction.
                /// </summary>
                ERROR_CTX_ACCOUNT_RESTRICTION = 7064,
                /// <summary>
                /// The RDP protocol component %2 detected an error in the protocol stream and has disconnected the client.
                /// </summary>
                ERROR_RDP_PROTOCOL_ERROR = 7065,
                /// <summary>
                /// The Client Drive Mapping Service Has Connected on Terminal Connection.
                /// </summary>
                ERROR_CTX_CDM_CONNECT = 7066,
                /// <summary>
                /// The Client Drive Mapping Service Has Disconnected on Terminal Connection.
                /// </summary>
                ERROR_CTX_CDM_DISCONNECT = 7067,
                /// <summary>
                /// The Terminal Server security layer detected an error in the protocol stream and has disconnected the client.
                /// </summary>
                ERROR_CTX_SECURITY_LAYER_ERROR = 7068,
                /// <summary>
                /// The target session is incompatible with the current session.
                /// </summary>
                ERROR_TS_INCOMPATIBLE_SESSIONS = 7069,
                /// <summary>
                /// Windows can't connect to your session because a problem occurred in the Windows video subsystem. Try connecting again later, or contact the server administrator for assistance.
                /// </summary>
                ERROR_TS_VIDEO_SUBSYSTEM_ERROR = 7070,
                /// <summary>
                /// The file replication service API was called incorrectly.
                /// </summary>
                FRS_ERR_INVALID_API_SEQUENCE = 8001,
                /// <summary>
                /// The file replication service cannot be started.
                /// </summary>
                FRS_ERR_STARTING_SERVICE = 8002,
                /// <summary>
                /// The file replication service cannot be stopped.
                /// </summary>
                FRS_ERR_STOPPING_SERVICE = 8003,
                /// <summary>
                /// The file replication service API terminated the request. The event log may have more information.
                /// </summary>
                FRS_ERR_INTERNAL_API = 8004,
                /// <summary>
                /// The file replication service terminated the request. The event log may have more information.
                /// </summary>
                FRS_ERR_INTERNAL = 8005,
                /// <summary>
                /// The file replication service cannot be contacted. The event log may have more information.
                /// </summary>
                FRS_ERR_SERVICE_COMM = 8006,
                /// <summary>
                /// The file replication service cannot satisfy the request because the user has insufficient privileges. The event log may have more information.
                /// </summary>
                FRS_ERR_INSUFFICIENT_PRIV = 8007,
                /// <summary>
                /// The file replication service cannot satisfy the request because authenticated RPC is not available. The event log may have more information.
                /// </summary>
                FRS_ERR_AUTHENTICATION = 8008,
                /// <summary>
                /// The file replication service cannot satisfy the request because the user has insufficient privileges on the domain controller. The event log may have more information.
                /// </summary>
                FRS_ERR_PARENT_INSUFFICIENT_PRIV = 8009,
                /// <summary>
                /// The file replication service cannot satisfy the request because authenticated RPC is not available on the domain controller. The event log may have more information.
                /// </summary>
                FRS_ERR_PARENT_AUTHENTICATION = 8010,
                /// <summary>
                /// The file replication service cannot communicate with the file replication service on the domain controller. The event log may have more information.
                /// </summary>
                FRS_ERR_CHILD_TO_PARENT_COMM = 8011,
                /// <summary>
                /// The file replication service on the domain controller cannot communicate with the file replication service on this computer. The event log may have more information.
                /// </summary>
                FRS_ERR_PARENT_TO_CHILD_COMM = 8012,
                /// <summary>
                /// The file replication service cannot populate the system volume because of an internal error. The event log may have more information.
                /// </summary>
                FRS_ERR_SYSVOL_POPULATE = 8013,
                /// <summary>
                /// The file replication service cannot populate the system volume because of an internal timeout. The event log may have more information.
                /// </summary>
                FRS_ERR_SYSVOL_POPULATE_TIMEOUT = 8014,
                /// <summary>
                /// The file replication service cannot process the request. The system volume is busy with a previous request.
                /// </summary>
                FRS_ERR_SYSVOL_IS_BUSY = 8015,
                /// <summary>
                /// The file replication service cannot stop replicating the system volume because of an internal error. The event log may have more information.
                /// </summary>
                FRS_ERR_SYSVOL_DEMOTE = 8016,
                /// <summary>
                /// The file replication service detected an invalid parameter.
                /// </summary>
                FRS_ERR_INVALID_SERVICE_PARAMETER = 8017,
                /// <summary>
                /// An error occurred while installing the directory service. For more information, see the event log.
                /// </summary>
                ERROR_DS_NOT_INSTALLED = 8200,
                /// <summary>
                /// The directory service evaluated group memberships locally.
                /// </summary>
                ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY = 8201,
                /// <summary>
                /// The specified directory service attribute or value does not exist.
                /// </summary>
                ERROR_DS_NO_ATTRIBUTE_OR_VALUE = 8202,
                /// <summary>
                /// The attribute syntax specified to the directory service is invalid.
                /// </summary>
                ERROR_DS_INVALID_ATTRIBUTE_SYNTAX = 8203,
                /// <summary>
                /// The attribute type specified to the directory service is not defined.
                /// </summary>
                ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED = 8204,
                /// <summary>
                /// The specified directory service attribute or value already exists.
                /// </summary>
                ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS = 8205,
                /// <summary>
                /// The directory service is busy.
                /// </summary>
                ERROR_DS_BUSY = 8206,
                /// <summary>
                /// The directory service is unavailable.
                /// </summary>
                ERROR_DS_UNAVAILABLE = 8207,
                /// <summary>
                /// The directory service was unable to allocate a relative identifier.
                /// </summary>
                ERROR_DS_NO_RIDS_ALLOCATED = 8208,
                /// <summary>
                /// The directory service has exhausted the pool of relative identifiers.
                /// </summary>
                ERROR_DS_NO_MORE_RIDS = 8209,
                /// <summary>
                /// The requested operation could not be performed because the directory service is not the master for that type of operation.
                /// </summary>
                ERROR_DS_INCORRECT_ROLE_OWNER = 8210,
                /// <summary>
                /// The directory service was unable to initialize the subsystem that allocates relative identifiers.
                /// </summary>
                ERROR_DS_RIDMGR_INIT_ERROR = 8211,
                /// <summary>
                /// The requested operation did not satisfy one or more constraints associated with the class of the object.
                /// </summary>
                ERROR_DS_OBJ_CLASS_VIOLATION = 8212,
                /// <summary>
                /// The directory service can perform the requested operation only on a leaf object.
                /// </summary>
                ERROR_DS_CANT_ON_NON_LEAF = 8213,
                /// <summary>
                /// The directory service cannot perform the requested operation on the RDN attribute of an object.
                /// </summary>
                ERROR_DS_CANT_ON_RDN = 8214,
                /// <summary>
                /// The directory service detected an attempt to modify the object class of an object.
                /// </summary>
                ERROR_DS_CANT_MOD_OBJ_CLASS = 8215,
                /// <summary>
                /// The requested cross-domain move operation could not be performed.
                /// </summary>
                ERROR_DS_CROSS_DOM_MOVE_ERROR = 8216,
                /// <summary>
                /// Unable to contact the global catalog server.
                /// </summary>
                ERROR_DS_GC_NOT_AVAILABLE = 8217,
                /// <summary>
                /// The policy object is shared and can only be modified at the root.
                /// </summary>
                ERROR_SHARED_POLICY = 8218,
                /// <summary>
                /// The policy object does not exist.
                /// </summary>
                ERROR_POLICY_OBJECT_NOT_FOUND = 8219,
                /// <summary>
                /// The requested policy information is only in the directory service.
                /// </summary>
                ERROR_POLICY_ONLY_IN_DS = 8220,
                /// <summary>
                /// A domain controller promotion is currently active.
                /// </summary>
                ERROR_PROMOTION_ACTIVE = 8221,
                /// <summary>
                /// A domain controller promotion is not currently active.
                /// </summary>
                ERROR_NO_PROMOTION_ACTIVE = 8222,
                /// <summary>
                /// An operations error occurred.
                /// </summary>
                ERROR_DS_OPERATIONS_ERROR = 8224,
                /// <summary>
                /// A protocol error occurred.
                /// </summary>
                ERROR_DS_PROTOCOL_ERROR = 8225,
                /// <summary>
                /// The time limit for this request was exceeded.
                /// </summary>
                ERROR_DS_TIMELIMIT_EXCEEDED = 8226,
                /// <summary>
                /// The size limit for this request was exceeded.
                /// </summary>
                ERROR_DS_SIZELIMIT_EXCEEDED = 8227,
                /// <summary>
                /// The administrative limit for this request was exceeded.
                /// </summary>
                ERROR_DS_ADMIN_LIMIT_EXCEEDED = 8228,
                /// <summary>
                /// The compare response was false.
                /// </summary>
                ERROR_DS_COMPARE_FALSE = 8229,
                /// <summary>
                /// The compare response was true.
                /// </summary>
                ERROR_DS_COMPARE_TRUE = 8230,
                /// <summary>
                /// The requested authentication method is not supported by the server.
                /// </summary>
                ERROR_DS_AUTH_METHOD_NOT_SUPPORTED = 8231,
                /// <summary>
                /// A more secure authentication method is required for this server.
                /// </summary>
                ERROR_DS_STRONG_AUTH_REQUIRED = 8232,
                /// <summary>
                /// Inappropriate authentication.
                /// </summary>
                ERROR_DS_INAPPROPRIATE_AUTH = 8233,
                /// <summary>
                /// The authentication mechanism is unknown.
                /// </summary>
                ERROR_DS_AUTH_UNKNOWN = 8234,
                /// <summary>
                /// A referral was returned from the server.
                /// </summary>
                ERROR_DS_REFERRAL = 8235,
                /// <summary>
                /// The server does not support the requested critical extension.
                /// </summary>
                ERROR_DS_UNAVAILABLE_CRIT_EXTENSION = 8236,
                /// <summary>
                /// This request requires a secure connection.
                /// </summary>
                ERROR_DS_CONFIDENTIALITY_REQUIRED = 8237,
                /// <summary>
                /// Inappropriate matching.
                /// </summary>
                ERROR_DS_INAPPROPRIATE_MATCHING = 8238,
                /// <summary>
                /// A constraint violation occurred.
                /// </summary>
                ERROR_DS_CONSTRAINT_VIOLATION = 8239,
                /// <summary>
                /// There is no such object on the server.
                /// </summary>
                ERROR_DS_NO_SUCH_OBJECT = 8240,
                /// <summary>
                /// There is an alias problem.
                /// </summary>
                ERROR_DS_ALIAS_PROBLEM = 8241,
                /// <summary>
                /// An invalid dn syntax has been specified.
                /// </summary>
                ERROR_DS_INVALID_DN_SYNTAX = 8242,
                /// <summary>
                /// The object is a leaf object.
                /// </summary>
                ERROR_DS_IS_LEAF = 8243,
                /// <summary>
                /// There is an alias dereferencing problem.
                /// </summary>
                ERROR_DS_ALIAS_DEREF_PROBLEM = 8244,
                /// <summary>
                /// The server is unwilling to process the request.
                /// </summary>
                ERROR_DS_UNWILLING_TO_PERFORM = 8245,
                /// <summary>
                /// A loop has been detected.
                /// </summary>
                ERROR_DS_LOOP_DETECT = 8246,
                /// <summary>
                /// There is a naming violation.
                /// </summary>
                ERROR_DS_NAMING_VIOLATION = 8247,
                /// <summary>
                /// The result set is too large.
                /// </summary>
                ERROR_DS_OBJECT_RESULTS_TOO_LARGE = 8248,
                /// <summary>
                /// The operation affects multiple DSAs.
                /// </summary>
                ERROR_DS_AFFECTS_MULTIPLE_DSAS = 8249,
                /// <summary>
                /// The server is not operational.
                /// </summary>
                ERROR_DS_SERVER_DOWN = 8250,
                /// <summary>
                /// A local error has occurred.
                /// </summary>
                ERROR_DS_LOCAL_ERROR = 8251,
                /// <summary>
                /// An encoding error has occurred.
                /// </summary>
                ERROR_DS_ENCODING_ERROR = 8252,
                /// <summary>
                /// A decoding error has occurred.
                /// </summary>
                ERROR_DS_DECODING_ERROR = 8253,
                /// <summary>
                /// The search filter cannot be recognized.
                /// </summary>
                ERROR_DS_FILTER_UNKNOWN = 8254,
                /// <summary>
                /// One or more parameters are illegal.
                /// </summary>
                ERROR_DS_PARAM_ERROR = 8255,
                /// <summary>
                /// The specified method is not supported.
                /// </summary>
                ERROR_DS_NOT_SUPPORTED = 8256,
                /// <summary>
                /// No results were returned.
                /// </summary>
                ERROR_DS_NO_RESULTS_RETURNED = 8257,
                /// <summary>
                /// The specified control is not supported by the server.
                /// </summary>
                ERROR_DS_CONTROL_NOT_FOUND = 8258,
                /// <summary>
                /// A referral loop was detected by the client.
                /// </summary>
                ERROR_DS_CLIENT_LOOP = 8259,
                /// <summary>
                /// The preset referral limit was exceeded.
                /// </summary>
                ERROR_DS_REFERRAL_LIMIT_EXCEEDED = 8260,
                /// <summary>
                /// The search requires a SORT control.
                /// </summary>
                ERROR_DS_SORT_CONTROL_MISSING = 8261,
                /// <summary>
                /// The search results exceed the offset range specified.
                /// </summary>
                ERROR_DS_OFFSET_RANGE_ERROR = 8262,
                /// <summary>
                /// The directory service detected the subsystem that allocates relative identifiers is disabled. This can occur as a protective mechanism when the system determines a significant portion of relative identifiers (RIDs) have been exhausted. Please see http://go.microsoft.com/fwlink/p/?linkid=228610 for recommended diagnostic steps and the procedure to re-enable account creation.
                /// </summary>
                ERROR_DS_RIDMGR_DISABLED = 8263,
                /// <summary>
                /// The root object must be the head of a naming context. The root object cannot have an instantiated parent.
                /// </summary>
                ERROR_DS_ROOT_MUST_BE_NC = 8301,
                /// <summary>
                /// The add replica operation cannot be performed. The naming context must be writeable in order to create the replica.
                /// </summary>
                ERROR_DS_ADD_REPLICA_INHIBITED = 8302,
                /// <summary>
                /// A reference to an attribute that is not defined in the schema occurred.
                /// </summary>
                ERROR_DS_ATT_NOT_DEF_IN_SCHEMA = 8303,
                /// <summary>
                /// The maximum size of an object has been exceeded.
                /// </summary>
                ERROR_DS_MAX_OBJ_SIZE_EXCEEDED = 8304,
                /// <summary>
                /// An attempt was made to add an object to the directory with a name that is already in use.
                /// </summary>
                ERROR_DS_OBJ_STRING_NAME_EXISTS = 8305,
                /// <summary>
                /// An attempt was made to add an object of a class that does not have an RDN defined in the schema.
                /// </summary>
                ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA = 8306,
                /// <summary>
                /// An attempt was made to add an object using an RDN that is not the RDN defined in the schema.
                /// </summary>
                ERROR_DS_RDN_DOESNT_MATCH_SCHEMA = 8307,
                /// <summary>
                /// None of the requested attributes were found on the objects.
                /// </summary>
                ERROR_DS_NO_REQUESTED_ATTS_FOUND = 8308,
                /// <summary>
                /// The user buffer is too small.
                /// </summary>
                ERROR_DS_USER_BUFFER_TO_SMALL = 8309,
                /// <summary>
                /// The attribute specified in the operation is not present on the object.
                /// </summary>
                ERROR_DS_ATT_IS_NOT_ON_OBJ = 8310,
                /// <summary>
                /// Illegal modify operation. Some aspect of the modification is not permitted.
                /// </summary>
                ERROR_DS_ILLEGAL_MOD_OPERATION = 8311,
                /// <summary>
                /// The specified object is too large.
                /// </summary>
                ERROR_DS_OBJ_TOO_LARGE = 8312,
                /// <summary>
                /// The specified instance type is not valid.
                /// </summary>
                ERROR_DS_BAD_INSTANCE_TYPE = 8313,
                /// <summary>
                /// The operation must be performed at a master DSA.
                /// </summary>
                ERROR_DS_MASTERDSA_REQUIRED = 8314,
                /// <summary>
                /// The object class attribute must be specified.
                /// </summary>
                ERROR_DS_OBJECT_CLASS_REQUIRED = 8315,
                /// <summary>
                /// A required attribute is missing.
                /// </summary>
                ERROR_DS_MISSING_REQUIRED_ATT = 8316,
                /// <summary>
                /// An attempt was made to modify an object to include an attribute that is not legal for its class.
                /// </summary>
                ERROR_DS_ATT_NOT_DEF_FOR_CLASS = 8317,
                /// <summary>
                /// The specified attribute is already present on the object.
                /// </summary>
                ERROR_DS_ATT_ALREADY_EXISTS = 8318,
                /// <summary>
                /// The specified attribute is not present, or has no values.
                /// </summary>
                ERROR_DS_CANT_ADD_ATT_VALUES = 8320,
                /// <summary>
                /// Multiple values were specified for an attribute that can have only one value.
                /// </summary>
                ERROR_DS_SINGLE_VALUE_CONSTRAINT = 8321,
                /// <summary>
                /// A value for the attribute was not in the acceptable range of values.
                /// </summary>
                ERROR_DS_RANGE_CONSTRAINT = 8322,
                /// <summary>
                /// The specified value already exists.
                /// </summary>
                ERROR_DS_ATT_VAL_ALREADY_EXISTS = 8323,
                /// <summary>
                /// The attribute cannot be removed because it is not present on the object.
                /// </summary>
                ERROR_DS_CANT_REM_MISSING_ATT = 8324,
                /// <summary>
                /// The attribute value cannot be removed because it is not present on the object.
                /// </summary>
                ERROR_DS_CANT_REM_MISSING_ATT_VAL = 8325,
                /// <summary>
                /// The specified root object cannot be a subref.
                /// </summary>
                ERROR_DS_ROOT_CANT_BE_SUBREF = 8326,
                /// <summary>
                /// Chaining is not permitted.
                /// </summary>
                ERROR_DS_NO_CHAINING = 8327,
                /// <summary>
                /// Chained evaluation is not permitted.
                /// </summary>
                ERROR_DS_NO_CHAINED_EVAL = 8328,
                /// <summary>
                /// The operation could not be performed because the object's parent is either uninstantiated or deleted.
                /// </summary>
                ERROR_DS_NO_PARENT_OBJECT = 8329,
                /// <summary>
                /// Having a parent that is an alias is not permitted. Aliases are leaf objects.
                /// </summary>
                ERROR_DS_PARENT_IS_AN_ALIAS = 8330,
                /// <summary>
                /// The object and parent must be of the same type, either both masters or both replicas.
                /// </summary>
                ERROR_DS_CANT_MIX_MASTER_AND_REPS = 8331,
                /// <summary>
                /// The operation cannot be performed because child objects exist. This operation can only be performed on a leaf object.
                /// </summary>
                ERROR_DS_CHILDREN_EXIST = 8332,
                /// <summary>
                /// Directory object not found.
                /// </summary>
                ERROR_DS_OBJ_NOT_FOUND = 8333,
                /// <summary>
                /// The aliased object is missing.
                /// </summary>
                ERROR_DS_ALIASED_OBJ_MISSING = 8334,
                /// <summary>
                /// The object name has bad syntax.
                /// </summary>
                ERROR_DS_BAD_NAME_SYNTAX = 8335,
                /// <summary>
                /// It is not permitted for an alias to refer to another alias.
                /// </summary>
                ERROR_DS_ALIAS_POINTS_TO_ALIAS = 8336,
                /// <summary>
                /// The alias cannot be dereferenced.
                /// </summary>
                ERROR_DS_CANT_DEREF_ALIAS = 8337,
                /// <summary>
                /// The operation is out of scope.
                /// </summary>
                ERROR_DS_OUT_OF_SCOPE = 8338,
                /// <summary>
                /// The operation cannot continue because the object is in the process of being removed.
                /// </summary>
                ERROR_DS_OBJECT_BEING_REMOVED = 8339,
                /// <summary>
                /// The DSA object cannot be deleted.
                /// </summary>
                ERROR_DS_CANT_DELETE_DSA_OBJ = 8340,
                /// <summary>
                /// A directory service error has occurred.
                /// </summary>
                ERROR_DS_GENERIC_ERROR = 8341,
                /// <summary>
                /// The operation can only be performed on an internal master DSA object.
                /// </summary>
                ERROR_DS_DSA_MUST_BE_INT_MASTER = 8342,
                /// <summary>
                /// The object must be of class DSA.
                /// </summary>
                ERROR_DS_CLASS_NOT_DSA = 8343,
                /// <summary>
                /// Insufficient access rights to perform the operation.
                /// </summary>
                ERROR_DS_INSUFF_ACCESS_RIGHTS = 8344,
                /// <summary>
                /// The object cannot be added because the parent is not on the list of possible superiors.
                /// </summary>
                ERROR_DS_ILLEGAL_SUPERIOR = 8345,
                /// <summary>
                /// Access to the attribute is not permitted because the attribute is owned by the Security Accounts Manager (SAM).
                /// </summary>
                ERROR_DS_ATTRIBUTE_OWNED_BY_SAM = 8346,
                /// <summary>
                /// The name has too many parts.
                /// </summary>
                ERROR_DS_NAME_TOO_MANY_PARTS = 8347,
                /// <summary>
                /// The name is too long.
                /// </summary>
                ERROR_DS_NAME_TOO_LONG = 8348,
                /// <summary>
                /// The name value is too long.
                /// </summary>
                ERROR_DS_NAME_VALUE_TOO_LONG = 8349,
                /// <summary>
                /// The directory service encountered an error parsing a name.
                /// </summary>
                ERROR_DS_NAME_UNPARSEABLE = 8350,
                /// <summary>
                /// The directory service cannot get the attribute type for a name.
                /// </summary>
                ERROR_DS_NAME_TYPE_UNKNOWN = 8351,
                /// <summary>
                /// The name does not identify an object; the name identifies a phantom.
                /// </summary>
                ERROR_DS_NOT_AN_OBJECT = 8352,
                /// <summary>
                /// The security descriptor is too short.
                /// </summary>
                ERROR_DS_SEC_DESC_TOO_SHORT = 8353,
                /// <summary>
                /// The security descriptor is invalid.
                /// </summary>
                ERROR_DS_SEC_DESC_INVALID = 8354,
                /// <summary>
                /// Failed to create name for deleted object.
                /// </summary>
                ERROR_DS_NO_DELETED_NAME = 8355,
                /// <summary>
                /// The parent of a new subref must exist.
                /// </summary>
                ERROR_DS_SUBREF_MUST_HAVE_PARENT = 8356,
                /// <summary>
                /// The object must be a naming context.
                /// </summary>
                ERROR_DS_NCNAME_MUST_BE_NC = 8357,
                /// <summary>
                /// It is not permitted to add an attribute which is owned by the system.
                /// </summary>
                ERROR_DS_CANT_ADD_SYSTEM_ONLY = 8358,
                /// <summary>
                /// The class of the object must be structural; you cannot instantiate an abstract class.
                /// </summary>
                ERROR_DS_CLASS_MUST_BE_CONCRETE = 8359,
                /// <summary>
                /// The schema object could not be found.
                /// </summary>
                ERROR_DS_INVALID_DMD = 8360,
                /// <summary>
                /// A local object with this GUID (dead or alive) already exists.
                /// </summary>
                ERROR_DS_OBJ_GUID_EXISTS = 8361,
                /// <summary>
                /// The operation cannot be performed on a back link.
                /// </summary>
                ERROR_DS_NOT_ON_BACKLINK = 8362,
                /// <summary>
                /// The cross reference for the specified naming context could not be found.
                /// </summary>
                ERROR_DS_NO_CROSSREF_FOR_NC = 8363,
                /// <summary>
                /// The operation could not be performed because the directory service is shutting down.
                /// </summary>
                ERROR_DS_SHUTTING_DOWN = 8364,
                /// <summary>
                /// The directory service request is invalid.
                /// </summary>
                ERROR_DS_UNKNOWN_OPERATION = 8365,
                /// <summary>
                /// The role owner attribute could not be read.
                /// </summary>
                ERROR_DS_INVALID_ROLE_OWNER = 8366,
                /// <summary>
                /// The requested FSMO operation failed. The current FSMO holder could not be contacted.
                /// </summary>
                ERROR_DS_COULDNT_CONTACT_FSMO = 8367,
                /// <summary>
                /// Modification of a DN across a naming context is not permitted.
                /// </summary>
                ERROR_DS_CROSS_NC_DN_RENAME = 8368,
                /// <summary>
                /// The attribute cannot be modified because it is owned by the system.
                /// </summary>
                ERROR_DS_CANT_MOD_SYSTEM_ONLY = 8369,
                /// <summary>
                /// Only the replicator can perform this function.
                /// </summary>
                ERROR_DS_REPLICATOR_ONLY = 8370,
                /// <summary>
                /// The specified class is not defined.
                /// </summary>
                ERROR_DS_OBJ_CLASS_NOT_DEFINED = 8371,
                /// <summary>
                /// The specified class is not a subclass.
                /// </summary>
                ERROR_DS_OBJ_CLASS_NOT_SUBCLASS = 8372,
                /// <summary>
                /// The name reference is invalid.
                /// </summary>
                ERROR_DS_NAME_REFERENCE_INVALID = 8373,
                /// <summary>
                /// A cross reference already exists.
                /// </summary>
                ERROR_DS_CROSS_REF_EXISTS = 8374,
                /// <summary>
                /// It is not permitted to delete a master cross reference.
                /// </summary>
                ERROR_DS_CANT_DEL_MASTER_CROSSREF = 8375,
                /// <summary>
                /// Subtree notifications are only supported on NC heads.
                /// </summary>
                ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD = 8376,
                /// <summary>
                /// Notification filter is too complex.
                /// </summary>
                ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX = 8377,
                /// <summary>
                /// Schema update failed: duplicate RDN.
                /// </summary>
                ERROR_DS_DUP_RDN = 8378,
                /// <summary>
                /// Schema update failed: duplicate OID.
                /// </summary>
                ERROR_DS_DUP_OID = 8379,
                /// <summary>
                /// Schema update failed: duplicate MAPI identifier.
                /// </summary>
                ERROR_DS_DUP_MAPI_ID = 8380,
                /// <summary>
                /// Schema update failed: duplicate schema-id GUID.
                /// </summary>
                ERROR_DS_DUP_SCHEMA_ID_GUID = 8381,
                /// <summary>
                /// Schema update failed: duplicate LDAP display name.
                /// </summary>
                ERROR_DS_DUP_LDAP_DISPLAY_NAME = 8382,
                /// <summary>
                /// Schema update failed: range-lower less than range upper.
                /// </summary>
                ERROR_DS_SEMANTIC_ATT_TEST = 8383,
                /// <summary>
                /// Schema update failed: syntax mismatch.
                /// </summary>
                ERROR_DS_SYNTAX_MISMATCH = 8384,
                /// <summary>
                /// Schema deletion failed: attribute is used in must-contain.
                /// </summary>
                ERROR_DS_EXISTS_IN_MUST_HAVE = 8385,
                /// <summary>
                /// Schema deletion failed: attribute is used in may-contain.
                /// </summary>
                ERROR_DS_EXISTS_IN_MAY_HAVE = 8386,
                /// <summary>
                /// Schema update failed: attribute in may-contain does not exist.
                /// </summary>
                ERROR_DS_NONEXISTENT_MAY_HAVE = 8387,
                /// <summary>
                /// Schema update failed: attribute in must-contain does not exist.
                /// </summary>
                ERROR_DS_NONEXISTENT_MUST_HAVE = 8388,
                /// <summary>
                /// Schema update failed: class in aux-class list does not exist or is not an auxiliary class.
                /// </summary>
                ERROR_DS_AUX_CLS_TEST_FAIL = 8389,
                /// <summary>
                /// Schema update failed: class in poss-superiors does not exist.
                /// </summary>
                ERROR_DS_NONEXISTENT_POSS_SUP = 8390,
                /// <summary>
                /// Schema update failed: class in subclassof list does not exist or does not satisfy hierarchy rules.
                /// </summary>
                ERROR_DS_SUB_CLS_TEST_FAIL = 8391,
                /// <summary>
                /// Schema update failed: Rdn-Att-Id has wrong syntax.
                /// </summary>
                ERROR_DS_BAD_RDN_ATT_ID_SYNTAX = 8392,
                /// <summary>
                /// Schema deletion failed: class is used as auxiliary class.
                /// </summary>
                ERROR_DS_EXISTS_IN_AUX_CLS = 8393,
                /// <summary>
                /// Schema deletion failed: class is used as sub class.
                /// </summary>
                ERROR_DS_EXISTS_IN_SUB_CLS = 8394,
                /// <summary>
                /// Schema deletion failed: class is used as poss superior.
                /// </summary>
                ERROR_DS_EXISTS_IN_POSS_SUP = 8395,
                /// <summary>
                /// Schema update failed in recalculating validation cache.
                /// </summary>
                ERROR_DS_RECALCSCHEMA_FAILED = 8396,
                /// <summary>
                /// The tree deletion is not finished. The request must be made again to continue deleting the tree.
                /// </summary>
                ERROR_DS_TREE_DELETE_NOT_FINISHED = 8397,
                /// <summary>
                /// The requested delete operation could not be performed.
                /// </summary>
                ERROR_DS_CANT_DELETE = 8398,
                /// <summary>
                /// Cannot read the governs class identifier for the schema record.
                /// </summary>
                ERROR_DS_ATT_SCHEMA_REQ_ID = 8399,
                /// <summary>
                /// The attribute schema has bad syntax.
                /// </summary>
                ERROR_DS_BAD_ATT_SCHEMA_SYNTAX = 8400,
                /// <summary>
                /// The attribute could not be cached.
                /// </summary>
                ERROR_DS_CANT_CACHE_ATT = 8401,
                /// <summary>
                /// The class could not be cached.
                /// </summary>
                ERROR_DS_CANT_CACHE_CLASS = 8402,
                /// <summary>
                /// The attribute could not be removed from the cache.
                /// </summary>
                ERROR_DS_CANT_REMOVE_ATT_CACHE = 8403,
                /// <summary>
                /// The class could not be removed from the cache.
                /// </summary>
                ERROR_DS_CANT_REMOVE_CLASS_CACHE = 8404,
                /// <summary>
                /// The distinguished name attribute could not be read.
                /// </summary>
                ERROR_DS_CANT_RETRIEVE_DN = 8405,
                /// <summary>
                /// No superior reference has been configured for the directory service. The directory service is therefore unable to issue referrals to objects outside this forest.
                /// </summary>
                ERROR_DS_MISSING_SUPREF = 8406,
                /// <summary>
                /// The instance type attribute could not be retrieved.
                /// </summary>
                ERROR_DS_CANT_RETRIEVE_INSTANCE = 8407,
                /// <summary>
                /// An internal error has occurred.
                /// </summary>
                ERROR_DS_CODE_INCONSISTENCY = 8408,
                /// <summary>
                /// A database error has occurred.
                /// </summary>
                ERROR_DS_DATABASE_ERROR = 8409,
                /// <summary>
                /// The attribute GOVERNSID is missing.
                /// </summary>
                ERROR_DS_GOVERNSID_MISSING = 8410,
                /// <summary>
                /// An expected attribute is missing.
                /// </summary>
                ERROR_DS_MISSING_EXPECTED_ATT = 8411,
                /// <summary>
                /// The specified naming context is missing a cross reference.
                /// </summary>
                ERROR_DS_NCNAME_MISSING_CR_REF = 8412,
                /// <summary>
                /// A security checking error has occurred.
                /// </summary>
                ERROR_DS_SECURITY_CHECKING_ERROR = 8413,
                /// <summary>
                /// The schema is not loaded.
                /// </summary>
                ERROR_DS_SCHEMA_NOT_LOADED = 8414,
                /// <summary>
                /// Schema allocation failed. Please check if the machine is running low on memory.
                /// </summary>
                ERROR_DS_SCHEMA_ALLOC_FAILED = 8415,
                /// <summary>
                /// Failed to obtain the required syntax for the attribute schema.
                /// </summary>
                ERROR_DS_ATT_SCHEMA_REQ_SYNTAX = 8416,
                /// <summary>
                /// The global catalog verification failed. The global catalog is not available or does not support the operation. Some part of the directory is currently not available.
                /// </summary>
                ERROR_DS_GCVERIFY_ERROR = 8417,
                /// <summary>
                /// The replication operation failed because of a schema mismatch between the servers involved.
                /// </summary>
                ERROR_DS_DRA_SCHEMA_MISMATCH = 8418,
                /// <summary>
                /// The DSA object could not be found.
                /// </summary>
                ERROR_DS_CANT_FIND_DSA_OBJ = 8419,
                /// <summary>
                /// The naming context could not be found.
                /// </summary>
                ERROR_DS_CANT_FIND_EXPECTED_NC = 8420,
                /// <summary>
                /// The naming context could not be found in the cache.
                /// </summary>
                ERROR_DS_CANT_FIND_NC_IN_CACHE = 8421,
                /// <summary>
                /// The child object could not be retrieved.
                /// </summary>
                ERROR_DS_CANT_RETRIEVE_CHILD = 8422,
                /// <summary>
                /// The modification was not permitted for security reasons.
                /// </summary>
                ERROR_DS_SECURITY_ILLEGAL_MODIFY = 8423,
                /// <summary>
                /// The operation cannot replace the hidden record.
                /// </summary>
                ERROR_DS_CANT_REPLACE_HIDDEN_REC = 8424,
                /// <summary>
                /// The hierarchy file is invalid.
                /// </summary>
                ERROR_DS_BAD_HIERARCHY_FILE = 8425,
                /// <summary>
                /// The attempt to build the hierarchy table failed.
                /// </summary>
                ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED = 8426,
                /// <summary>
                /// The directory configuration parameter is missing from the registry.
                /// </summary>
                ERROR_DS_CONFIG_PARAM_MISSING = 8427,
                /// <summary>
                /// The attempt to count the address book indices failed.
                /// </summary>
                ERROR_DS_COUNTING_AB_INDICES_FAILED = 8428,
                /// <summary>
                /// The allocation of the hierarchy table failed.
                /// </summary>
                ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED = 8429,
                /// <summary>
                /// The directory service encountered an internal failure.
                /// </summary>
                ERROR_DS_INTERNAL_FAILURE = 8430,
                /// <summary>
                /// The directory service encountered an unknown failure.
                /// </summary>
                ERROR_DS_UNKNOWN_ERROR = 8431,
                /// <summary>
                /// A root object requires a class of 'top'.
                /// </summary>
                ERROR_DS_ROOT_REQUIRES_CLASS_TOP = 8432,
                /// <summary>
                /// This directory server is shutting down, and cannot take ownership of new floating single-master operation roles.
                /// </summary>
                ERROR_DS_REFUSING_FSMO_ROLES = 8433,
                /// <summary>
                /// The directory service is missing mandatory configuration information, and is unable to determine the ownership of floating single-master operation roles.
                /// </summary>
                ERROR_DS_MISSING_FSMO_SETTINGS = 8434,
                /// <summary>
                /// The directory service was unable to transfer ownership of one or more floating single-master operation roles to other servers.
                /// </summary>
                ERROR_DS_UNABLE_TO_SURRENDER_ROLES = 8435,
                /// <summary>
                /// The replication operation failed.
                /// </summary>
                ERROR_DS_DRA_GENERIC = 8436,
                /// <summary>
                /// An invalid parameter was specified for this replication operation.
                /// </summary>
                ERROR_DS_DRA_INVALID_PARAMETER = 8437,
                /// <summary>
                /// The directory service is too busy to complete the replication operation at this time.
                /// </summary>
                ERROR_DS_DRA_BUSY = 8438,
                /// <summary>
                /// The distinguished name specified for this replication operation is invalid.
                /// </summary>
                ERROR_DS_DRA_BAD_DN = 8439,
                /// <summary>
                /// The naming context specified for this replication operation is invalid.
                /// </summary>
                ERROR_DS_DRA_BAD_NC = 8440,
                /// <summary>
                /// The distinguished name specified for this replication operation already exists.
                /// </summary>
                ERROR_DS_DRA_DN_EXISTS = 8441,
                /// <summary>
                /// The replication system encountered an internal error.
                /// </summary>
                ERROR_DS_DRA_INTERNAL_ERROR = 8442,
                /// <summary>
                /// The replication operation encountered a database inconsistency.
                /// </summary>
                ERROR_DS_DRA_INCONSISTENT_DIT = 8443,
                /// <summary>
                /// The server specified for this replication operation could not be contacted.
                /// </summary>
                ERROR_DS_DRA_CONNECTION_FAILED = 8444,
                /// <summary>
                /// The replication operation encountered an object with an invalid instance type.
                /// </summary>
                ERROR_DS_DRA_BAD_INSTANCE_TYPE = 8445,
                /// <summary>
                /// The replication operation failed to allocate memory.
                /// </summary>
                ERROR_DS_DRA_OUT_OF_MEM = 8446,
                /// <summary>
                /// The replication operation encountered an error with the mail system.
                /// </summary>
                ERROR_DS_DRA_MAIL_PROBLEM = 8447,
                /// <summary>
                /// The replication reference information for the target server already exists.
                /// </summary>
                ERROR_DS_DRA_REF_ALREADY_EXISTS = 8448,
                /// <summary>
                /// The replication reference information for the target server does not exist.
                /// </summary>
                ERROR_DS_DRA_REF_NOT_FOUND = 8449,
                /// <summary>
                /// The naming context cannot be removed because it is replicated to another server.
                /// </summary>
                ERROR_DS_DRA_OBJ_IS_REP_SOURCE = 8450,
                /// <summary>
                /// The replication operation encountered a database error.
                /// </summary>
                ERROR_DS_DRA_DB_ERROR = 8451,
                /// <summary>
                /// The naming context is in the process of being removed or is not replicated from the specified server.
                /// </summary>
                ERROR_DS_DRA_NO_REPLICA = 8452,
                /// <summary>
                /// Replication access was denied.
                /// </summary>
                ERROR_DS_DRA_ACCESS_DENIED = 8453,
                /// <summary>
                /// The requested operation is not supported by this version of the directory service.
                /// </summary>
                ERROR_DS_DRA_NOT_SUPPORTED = 8454,
                /// <summary>
                /// The replication remote procedure call was cancelled.
                /// </summary>
                ERROR_DS_DRA_RPC_CANCELLED = 8455,
                /// <summary>
                /// The source server is currently rejecting replication requests.
                /// </summary>
                ERROR_DS_DRA_SOURCE_DISABLED = 8456,
                /// <summary>
                /// The destination server is currently rejecting replication requests.
                /// </summary>
                ERROR_DS_DRA_SINK_DISABLED = 8457,
                /// <summary>
                /// The replication operation failed due to a collision of object names.
                /// </summary>
                ERROR_DS_DRA_NAME_COLLISION = 8458,
                /// <summary>
                /// The replication source has been reinstalled.
                /// </summary>
                ERROR_DS_DRA_SOURCE_REINSTALLED = 8459,
                /// <summary>
                /// The replication operation failed because a required parent object is missing.
                /// </summary>
                ERROR_DS_DRA_MISSING_PARENT = 8460,
                /// <summary>
                /// The replication operation was preempted.
                /// </summary>
                ERROR_DS_DRA_PREEMPTED = 8461,
                /// <summary>
                /// The replication synchronization attempt was abandoned because of a lack of updates.
                /// </summary>
                ERROR_DS_DRA_ABANDON_SYNC = 8462,
                /// <summary>
                /// The replication operation was terminated because the system is shutting down.
                /// </summary>
                ERROR_DS_DRA_SHUTDOWN = 8463,
                /// <summary>
                /// Synchronization attempt failed because the destination DC is currently waiting to synchronize new partial attributes from source. This condition is normal if a recent schema change modified the partial attribute set. The destination partial attribute set is not a subset of source partial attribute set.
                /// </summary>
                ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET = 8464,
                /// <summary>
                /// The replication synchronization attempt failed because a master replica attempted to sync from a partial replica.
                /// </summary>
                ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA = 8465,
                /// <summary>
                /// The server specified for this replication operation was contacted, but that server was unable to contact an additional server needed to complete the operation.
                /// </summary>
                ERROR_DS_DRA_EXTN_CONNECTION_FAILED = 8466,
                /// <summary>
                /// The version of the directory service schema of the source forest is not compatible with the version of directory service on this computer.
                /// </summary>
                ERROR_DS_INSTALL_SCHEMA_MISMATCH = 8467,
                /// <summary>
                /// Schema update failed: An attribute with the same link identifier already exists.
                /// </summary>
                ERROR_DS_DUP_LINK_ID = 8468,
                /// <summary>
                /// Name translation: Generic processing error.
                /// </summary>
                ERROR_DS_NAME_ERROR_RESOLVING = 8469,
                /// <summary>
                /// Name translation: Could not find the name or insufficient right to see name.
                /// </summary>
                ERROR_DS_NAME_ERROR_NOT_FOUND = 8470,
                /// <summary>
                /// Name translation: Input name mapped to more than one output name.
                /// </summary>
                ERROR_DS_NAME_ERROR_NOT_UNIQUE = 8471,
                /// <summary>
                /// Name translation: Input name found, but not the associated output format.
                /// </summary>
                ERROR_DS_NAME_ERROR_NO_MAPPING = 8472,
                /// <summary>
                /// Name translation: Unable to resolve completely, only the domain was found.
                /// </summary>
                ERROR_DS_NAME_ERROR_DOMAIN_ONLY = 8473,
                /// <summary>
                /// Name translation: Unable to perform purely syntactical mapping at the client without going out to the wire.
                /// </summary>
                ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING = 8474,
                /// <summary>
                /// Modification of a constructed attribute is not allowed.
                /// </summary>
                ERROR_DS_CONSTRUCTED_ATT_MOD = 8475,
                /// <summary>
                /// The OM-Object-Class specified is incorrect for an attribute with the specified syntax.
                /// </summary>
                ERROR_DS_WRONG_OM_OBJ_CLASS = 8476,
                /// <summary>
                /// The replication request has been posted; waiting for reply.
                /// </summary>
                ERROR_DS_DRA_REPL_PENDING = 8477,
                /// <summary>
                /// The requested operation requires a directory service, and none was available.
                /// </summary>
                ERROR_DS_DS_REQUIRED = 8478,
                /// <summary>
                /// The LDAP display name of the class or attribute contains non-ASCII characters.
                /// </summary>
                ERROR_DS_INVALID_LDAP_DISPLAY_NAME = 8479,
                /// <summary>
                /// The requested search operation is only supported for base searches.
                /// </summary>
                ERROR_DS_NON_BASE_SEARCH = 8480,
                /// <summary>
                /// The search failed to retrieve attributes from the database.
                /// </summary>
                ERROR_DS_CANT_RETRIEVE_ATTS = 8481,
                /// <summary>
                /// The schema update operation tried to add a backward link attribute that has no corresponding forward link.
                /// </summary>
                ERROR_DS_BACKLINK_WITHOUT_LINK = 8482,
                /// <summary>
                /// Source and destination of a cross-domain move do not agree on the object's epoch number. Either source or destination does not have the latest version of the object.
                /// </summary>
                ERROR_DS_EPOCH_MISMATCH = 8483,
                /// <summary>
                /// Source and destination of a cross-domain move do not agree on the object's current name. Either source or destination does not have the latest version of the object.
                /// </summary>
                ERROR_DS_SRC_NAME_MISMATCH = 8484,
                /// <summary>
                /// Source and destination for the cross-domain move operation are identical. Caller should use local move operation instead of cross-domain move operation.
                /// </summary>
                ERROR_DS_SRC_AND_DST_NC_IDENTICAL = 8485,
                /// <summary>
                /// Source and destination for a cross-domain move are not in agreement on the naming contexts in the forest. Either source or destination does not have the latest version of the Partitions container.
                /// </summary>
                ERROR_DS_DST_NC_MISMATCH = 8486,
                /// <summary>
                /// Destination of a cross-domain move is not authoritative for the destination naming context.
                /// </summary>
                ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC = 8487,
                /// <summary>
                /// Source and destination of a cross-domain move do not agree on the identity of the source object. Either source or destination does not have the latest version of the source object.
                /// </summary>
                ERROR_DS_SRC_GUID_MISMATCH = 8488,
                /// <summary>
                /// Object being moved across-domains is already known to be deleted by the destination server. The source server does not have the latest version of the source object.
                /// </summary>
                ERROR_DS_CANT_MOVE_DELETED_OBJECT = 8489,
                /// <summary>
                /// Another operation which requires exclusive access to the PDC FSMO is already in progress.
                /// </summary>
                ERROR_DS_PDC_OPERATION_IN_PROGRESS = 8490,
                /// <summary>
                /// A cross-domain move operation failed such that two versions of the moved object exist - one each in the source and destination domains. The destination object needs to be removed to restore the system to a consistent state.
                /// </summary>
                ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD = 8491,
                /// <summary>
                /// This object may not be moved across domain boundaries either because cross-domain moves for this class are disallowed, or the object has some special characteristics, e.g.: trust account or restricted RID, which prevent its move.
                /// </summary>
                ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION = 8492,
                /// <summary>
                /// Can't move objects with memberships across domain boundaries as once moved, this would violate the membership conditions of the account group. Remove the object from any account group memberships and retry.
                /// </summary>
                ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS = 8493,
                /// <summary>
                /// A naming context head must be the immediate child of another naming context head, not of an interior node.
                /// </summary>
                ERROR_DS_NC_MUST_HAVE_NC_PARENT = 8494,
                /// <summary>
                /// The directory cannot validate the proposed naming context name because it does not hold a replica of the naming context above the proposed naming context. Please ensure that the domain naming master role is held by a server that is configured as a global catalog server, and that the server is up to date with its replication partners. (Applies only to Windows 2000 Domain Naming masters.)
                /// </summary>
                ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE = 8495,
                /// <summary>
                /// Destination domain must be in native mode.
                /// </summary>
                ERROR_DS_DST_DOMAIN_NOT_NATIVE = 8496,
                /// <summary>
                /// The operation cannot be performed because the server does not have an infrastructure container in the domain of interest.
                /// </summary>
                ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER = 8497,
                /// <summary>
                /// Cross-domain move of non-empty account groups is not allowed.
                /// </summary>
                ERROR_DS_CANT_MOVE_ACCOUNT_GROUP = 8498,
                /// <summary>
                /// Cross-domain move of non-empty resource groups is not allowed.
                /// </summary>
                ERROR_DS_CANT_MOVE_RESOURCE_GROUP = 8499,
                /// <summary>
                /// The search flags for the attribute are invalid. The ANR bit is valid only on attributes of Unicode or Teletex strings.
                /// </summary>
                ERROR_DS_INVALID_SEARCH_FLAG = 8500,
                /// <summary>
                /// Tree deletions starting at an object which has an NC head as a descendant are not allowed.
                /// </summary>
                ERROR_DS_NO_TREE_DELETE_ABOVE_NC = 8501,
                /// <summary>
                /// The directory service failed to lock a tree in preparation for a tree deletion because the tree was in use.
                /// </summary>
                ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE = 8502,
                /// <summary>
                /// The directory service failed to identify the list of objects to delete while attempting a tree deletion.
                /// </summary>
                ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE = 8503,
                /// <summary>
                /// Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Please shutdown this system and reboot into Directory Services Restore Mode, check the event log for more  detailed information.
                /// </summary>
                ERROR_DS_SAM_INIT_FAILURE = 8504,
                /// <summary>
                /// Only an administrator can modify the membership list of an administrative group.
                /// </summary>
                ERROR_DS_SENSITIVE_GROUP_VIOLATION = 8505,
                /// <summary>
                /// Cannot change the primary group ID of a domain controller account.
                /// </summary>
                ERROR_DS_CANT_MOD_PRIMARYGROUPID = 8506,
                /// <summary>
                /// An attempt is made to modify the base schema.
                /// </summary>
                ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD = 8507,
                /// <summary>
                /// Adding a new mandatory attribute to an existing class, deleting a mandatory attribute from an existing class, or adding an optional attribute to the special class Top that is not a backlink attribute (directly or  through inheritance, for example, by adding or deleting an auxiliary class) is not allowed.
                /// </summary>
                ERROR_DS_NONSAFE_SCHEMA_CHANGE = 8508,
                /// <summary>
                /// Schema update is not allowed on this DC because the DC is not the schema FSMO Role Owner.
                /// </summary>
                ERROR_DS_SCHEMA_UPDATE_DISALLOWED = 8509,
                /// <summary>
                /// An object of this class cannot be created under the schema container. You can only create attribute-schema and class-schema objects under the schema container.
                /// </summary>
                ERROR_DS_CANT_CREATE_UNDER_SCHEMA = 8510,
                /// <summary>
                /// The replica/child install failed to get the objectVersion attribute on the schema container on the source DC. Either the attribute is missing on the schema container or the credentials supplied do not have permission to read it.
                /// </summary>
                ERROR_DS_INSTALL_NO_SRC_SCH_VERSION = 8511,
                /// <summary>
                /// The replica/child install failed to read the objectVersion attribute in the SCHEMA section of the file schema.ini in the system32 directory.
                /// </summary>
                ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE = 8512,
                /// <summary>
                /// The specified group type is invalid.
                /// </summary>
                ERROR_DS_INVALID_GROUP_TYPE = 8513,
                /// <summary>
                /// You cannot nest global groups in a mixed domain if the group is security-enabled.
                /// </summary>
                ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = 8514,
                /// <summary>
                /// You cannot nest local groups in a mixed domain if the group is security-enabled.
                /// </summary>
                ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = 8515,
                /// <summary>
                /// A global group cannot have a local group as a member.
                /// </summary>
                ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER = 8516,
                /// <summary>
                /// A global group cannot have a universal group as a member.
                /// </summary>
                ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER = 8517,
                /// <summary>
                /// A universal group cannot have a local group as a member.
                /// </summary>
                ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER = 8518,
                /// <summary>
                /// A global group cannot have a cross-domain member.
                /// </summary>
                ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER = 8519,
                /// <summary>
                /// A local group cannot have another cross domain local group as a member.
                /// </summary>
                ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER = 8520,
                /// <summary>
                /// A group with primary members cannot change to a security-disabled group.
                /// </summary>
                ERROR_DS_HAVE_PRIMARY_MEMBERS = 8521,
                /// <summary>
                /// The schema cache load failed to convert the string default SD on a class-schema object.
                /// </summary>
                ERROR_DS_STRING_SD_CONVERSION_FAILED = 8522,
                /// <summary>
                /// Only DSAs configured to be Global Catalog servers should be allowed to hold the Domain Naming Master FSMO role. (Applies only to Windows 2000 servers.)
                /// </summary>
                ERROR_DS_NAMING_MASTER_GC = 8523,
                /// <summary>
                /// The DSA operation is unable to proceed because of a DNS lookup failure.
                /// </summary>
                ERROR_DS_DNS_LOOKUP_FAILURE = 8524,
                /// <summary>
                /// While processing a change to the DNS Host Name for an object, the Service Principal Name values could not be kept in sync.
                /// </summary>
                ERROR_DS_COULDNT_UPDATE_SPNS = 8525,
                /// <summary>
                /// The Security Descriptor attribute could not be read.
                /// </summary>
                ERROR_DS_CANT_RETRIEVE_SD = 8526,
                /// <summary>
                /// The object requested was not found, but an object with that key was found.
                /// </summary>
                ERROR_DS_KEY_NOT_UNIQUE = 8527,
                /// <summary>
                /// The syntax of the linked attribute being added is incorrect. Forward links can only have syntax 2.5.5.1, 2.5.5.7, and 2.5.5.14, and backlinks can only have syntax 2.5.5.1.
                /// </summary>
                ERROR_DS_WRONG_LINKED_ATT_SYNTAX = 8528,
                /// <summary>
                /// Security Account Manager needs to get the boot password.
                /// </summary>
                ERROR_DS_SAM_NEED_BOOTKEY_PASSWORD = 8529,
                /// <summary>
                /// Security Account Manager needs to get the boot key from floppy disk.
                /// </summary>
                ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY = 8530,
                /// <summary>
                /// Directory Service cannot start.
                /// </summary>
                ERROR_DS_CANT_START = 8531,
                /// <summary>
                /// Directory Services could not start.
                /// </summary>
                ERROR_DS_INIT_FAILURE = 8532,
                /// <summary>
                /// The connection between client and server requires packet privacy or better.
                /// </summary>
                ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION = 8533,
                /// <summary>
                /// The source domain may not be in the same forest as destination.
                /// </summary>
                ERROR_DS_SOURCE_DOMAIN_IN_FOREST = 8534,
                /// <summary>
                /// The destination domain must be in the forest.
                /// </summary>
                ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST = 8535,
                /// <summary>
                /// The operation requires that destination domain auditing be enabled.
                /// </summary>
                ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED = 8536,
                /// <summary>
                /// The operation couldn't locate a DC for the source domain.
                /// </summary>
                ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN = 8537,
                /// <summary>
                /// The source object must be a group or user.
                /// </summary>
                ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER = 8538,
                /// <summary>
                /// The source object's SID already exists in destination forest.
                /// </summary>
                ERROR_DS_SRC_SID_EXISTS_IN_FOREST = 8539,
                /// <summary>
                /// The source and destination object must be of the same type.
                /// </summary>
                ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH = 8540,
                /// <summary>
                /// Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Click OK to shut down the system and reboot into Safe Mode. Check the event log for detailed information.
                /// </summary>
                ERROR_SAM_INIT_FAILURE = 8541,
                /// <summary>
                /// Schema information could not be included in the replication request.
                /// </summary>
                ERROR_DS_DRA_SCHEMA_INFO_SHIP = 8542,
                /// <summary>
                /// The replication operation could not be completed due to a schema incompatibility.
                /// </summary>
                ERROR_DS_DRA_SCHEMA_CONFLICT = 8543,
                /// <summary>
                /// The replication operation could not be completed due to a previous schema incompatibility.
                /// </summary>
                ERROR_DS_DRA_EARLIER_SCHEMA_CONFLICT = 8544,
                /// <summary>
                /// The replication update could not be applied because either the source or the destination has not yet received information regarding a recent cross-domain move operation.
                /// </summary>
                ERROR_DS_DRA_OBJ_NC_MISMATCH = 8545,
                /// <summary>
                /// The requested domain could not be deleted because there exist domain controllers that still host this domain.
                /// </summary>
                ERROR_DS_NC_STILL_HAS_DSAS = 8546,
                /// <summary>
                /// The requested operation can be performed only on a global catalog server.
                /// </summary>
                ERROR_DS_GC_REQUIRED = 8547,
                /// <summary>
                /// A local group can only be a member of other local groups in the same domain.
                /// </summary>
                ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY = 8548,
                /// <summary>
                /// Foreign security principals cannot be members of universal groups.
                /// </summary>
                ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS = 8549,
                /// <summary>
                /// The attribute is not allowed to be replicated to the GC because of security reasons.
                /// </summary>
                ERROR_DS_CANT_ADD_TO_GC = 8550,
                /// <summary>
                /// The checkpoint with the PDC could not be taken because there too many modifications being processed currently.
                /// </summary>
                ERROR_DS_NO_CHECKPOINT_WITH_PDC = 8551,
                /// <summary>
                /// The operation requires that source domain auditing be enabled.
                /// </summary>
                ERROR_DS_SOURCE_AUDITING_NOT_ENABLED = 8552,
                /// <summary>
                /// Security principal objects can only be created inside domain naming contexts.
                /// </summary>
                ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC = 8553,
                /// <summary>
                /// A Service Principal Name (SPN) could not be constructed because the provided hostname is not in the necessary format.
                /// </summary>
                ERROR_DS_INVALID_NAME_FOR_SPN = 8554,
                /// <summary>
                /// A Filter was passed that uses constructed attributes.
                /// </summary>
                ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS = 8555,
                /// <summary>
                /// The unicodePwd attribute value must be enclosed in double quotes.
                /// </summary>
                ERROR_DS_UNICODEPWD_NOT_IN_QUOTES = 8556,
                /// <summary>
                /// Your computer could not be joined to the domain. You have exceeded the maximum number of computer accounts you are allowed to create in this domain. Contact your system administrator to have this limit reset or increased.
                /// </summary>
                ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED = 8557,
                /// <summary>
                /// For security reasons, the operation must be run on the destination DC.
                /// </summary>
                ERROR_DS_MUST_BE_RUN_ON_DST_DC = 8558,
                /// <summary>
                /// For security reasons, the source DC must be NT4SP4 or greater.
                /// </summary>
                ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER = 8559,
                /// <summary>
                /// Critical Directory Service System objects cannot be deleted during tree delete operations. The tree delete may have been partially performed.
                /// </summary>
                ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ = 8560,
                /// <summary>
                /// Directory Services could not start because of the following error: %1. Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.
                /// </summary>
                ERROR_DS_INIT_FAILURE_CONSOLE = 8561,
                /// <summary>
                /// Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.
                /// </summary>
                ERROR_DS_SAM_INIT_FAILURE_CONSOLE = 8562,
                /// <summary>
                /// The version of the operating system is incompatible with the current AD DS forest functional level or AD LDS Configuration Set functional level. You must upgrade to a new version of the operating system before this server can become an AD DS Domain Controller or add an AD LDS Instance in this AD DS Forest or AD LDS Configuration Set.
                /// </summary>
                ERROR_DS_FOREST_VERSION_TOO_HIGH = 8563,
                /// <summary>
                /// The version of the operating system installed is incompatible with the current domain functional level. You must upgrade to a new version of the operating system before this server can become a domain controller in this domain.
                /// </summary>
                ERROR_DS_DOMAIN_VERSION_TOO_HIGH = 8564,
                /// <summary>
                /// The version of the operating system installed on this server no longer supports the current AD DS Forest functional level or AD LDS Configuration Set functional level. You must raise the AD DS Forest functional level or AD LDS Configuration Set functional level before this server can become an AD DS Domain Controller or an AD LDS Instance in this Forest or Configuration Set.
                /// </summary>
                ERROR_DS_FOREST_VERSION_TOO_LOW = 8565,
                /// <summary>
                /// The version of the operating system installed on this server no longer supports the current domain functional level. You must raise the domain functional level before this server can become a domain controller in this domain.
                /// </summary>
                ERROR_DS_DOMAIN_VERSION_TOO_LOW = 8566,
                /// <summary>
                /// The version of the operating system installed on this server is incompatible with the functional level of the domain or forest.
                /// </summary>
                ERROR_DS_INCOMPATIBLE_VERSION = 8567,
                /// <summary>
                /// The functional level of the domain (or forest) cannot be raised to the requested value, because there exist one or more domain controllers in the domain (or forest) that are at a lower incompatible functional level.
                /// </summary>
                ERROR_DS_LOW_DSA_VERSION = 8568,
                /// <summary>
                /// The forest functional level cannot be raised to the requested value since one or more domains are still in mixed domain mode. All domains in the forest must be in native mode, for you to raise the forest functional level.
                /// </summary>
                ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN = 8569,
                /// <summary>
                /// The sort order requested is not supported.
                /// </summary>
                ERROR_DS_NOT_SUPPORTED_SORT_ORDER = 8570,
                /// <summary>
                /// The requested name already exists as a unique identifier.
                /// </summary>
                ERROR_DS_NAME_NOT_UNIQUE = 8571,
                /// <summary>
                /// The machine account was created pre-NT4. The account needs to be recreated.
                /// </summary>
                ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 = 8572,
                /// <summary>
                /// The database is out of version store.
                /// </summary>
                ERROR_DS_OUT_OF_VERSION_STORE = 8573,
                /// <summary>
                /// Unable to continue operation because multiple conflicting controls were used.
                /// </summary>
                ERROR_DS_INCOMPATIBLE_CONTROLS_USED = 8574,
                /// <summary>
                /// Unable to find a valid security descriptor reference domain for this partition.
                /// </summary>
                ERROR_DS_NO_REF_DOMAIN = 8575,
                /// <summary>
                /// Schema update failed: The link identifier is reserved.
                /// </summary>
                ERROR_DS_RESERVED_LINK_ID = 8576,
                /// <summary>
                /// Schema update failed: There are no link identifiers available.
                /// </summary>
                ERROR_DS_LINK_ID_NOT_AVAILABLE = 8577,
                /// <summary>
                /// An account group cannot have a universal group as a member.
                /// </summary>
                ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER = 8578,
                /// <summary>
                /// Rename or move operations on naming context heads or read-only objects are not allowed.
                /// </summary>
                ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE = 8579,
                /// <summary>
                /// Move operations on objects in the schema naming context are not allowed.
                /// </summary>
                ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC = 8580,
                /// <summary>
                /// A system flag has been set on the object and does not allow the object to be moved or renamed.
                /// </summary>
                ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG = 8581,
                /// <summary>
                /// This object is not allowed to change its grandparent container. Moves are not forbidden on this object, but are restricted to sibling containers.
                /// </summary>
                ERROR_DS_MODIFYDN_WRONG_GRANDPARENT = 8582,
                /// <summary>
                /// Unable to resolve completely, a referral to another forest is generated.
                /// </summary>
                ERROR_DS_NAME_ERROR_TRUST_REFERRAL = 8583,
                /// <summary>
                /// The requested action is not supported on standard server.
                /// </summary>
                ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER = 8584,
                /// <summary>
                /// Could not access a partition of the directory service located on a remote server. Make sure at least one server is running for the partition in question.
                /// </summary>
                ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD = 8585,
                /// <summary>
                /// The directory cannot validate the proposed naming context (or partition) name because it does not hold a replica nor can it contact a replica of the naming context above the proposed naming context. Please ensure that the parent naming context is properly registered in DNS, and at least one replica of this naming context is reachable by the Domain Naming master.
                /// </summary>
                ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE_V2 = 8586,
                /// <summary>
                /// The thread limit for this request was exceeded.
                /// </summary>
                ERROR_DS_THREAD_LIMIT_EXCEEDED = 8587,
                /// <summary>
                /// The Global catalog server is not in the closest site.
                /// </summary>
                ERROR_DS_NOT_CLOSEST = 8588,
                /// <summary>
                /// The DS cannot derive a service principal name (SPN) with which to mutually authenticate the target server because the corresponding server object in the local DS database has no serverReference attribute.
                /// </summary>
                ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF = 8589,
                /// <summary>
                /// The Directory Service failed to enter single user mode.
                /// </summary>
                ERROR_DS_SINGLE_USER_MODE_FAILED = 8590,
                /// <summary>
                /// The Directory Service cannot parse the script because of a syntax error.
                /// </summary>
                ERROR_DS_NTDSCRIPT_SYNTAX_ERROR = 8591,
                /// <summary>
                /// The Directory Service cannot process the script because of an error.
                /// </summary>
                ERROR_DS_NTDSCRIPT_PROCESS_ERROR = 8592,
                /// <summary>
                /// The directory service cannot perform the requested operation because the servers involved are of different replication epochs (which is usually related to a domain rename that is in progress).
                /// </summary>
                ERROR_DS_DIFFERENT_REPL_EPOCHS = 8593,
                /// <summary>
                /// The directory service binding must be renegotiated due to a change in the server extensions information.
                /// </summary>
                ERROR_DS_DRS_EXTENSIONS_CHANGED = 8594,
                /// <summary>
                /// Operation not allowed on a disabled cross ref.
                /// </summary>
                ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR = 8595,
                /// <summary>
                /// Schema update failed: No values for msDS-IntId are available.
                /// </summary>
                ERROR_DS_NO_MSDS_INTID = 8596,
                /// <summary>
                /// Schema update failed: Duplicate msDS-INtId. Retry the operation.
                /// </summary>
                ERROR_DS_DUP_MSDS_INTID = 8597,
                /// <summary>
                /// Schema deletion failed: attribute is used in rDNAttID.
                /// </summary>
                ERROR_DS_EXISTS_IN_RDNATTID = 8598,
                /// <summary>
                /// The directory service failed to authorize the request.
                /// </summary>
                ERROR_DS_AUTHORIZATION_FAILED = 8599,
                /// <summary>
                /// The Directory Service cannot process the script because it is invalid.
                /// </summary>
                ERROR_DS_INVALID_SCRIPT = 8600,
                /// <summary>
                /// The remote create cross reference operation failed on the Domain Naming Master FSMO. The operation's error is in the extended data.
                /// </summary>
                ERROR_DS_REMOTE_CROSSREF_OP_FAILED = 8601,
                /// <summary>
                /// A cross reference is in use locally with the same name.
                /// </summary>
                ERROR_DS_CROSS_REF_BUSY = 8602,
                /// <summary>
                /// The DS cannot derive a service principal name (SPN) with which to mutually authenticate the target server because the server's domain has been deleted from the forest.
                /// </summary>
                ERROR_DS_CANT_DERIVE_SPN_FOR_DELETED_DOMAIN = 8603,
                /// <summary>
                /// Writeable NCs prevent this DC from demoting.
                /// </summary>
                ERROR_DS_CANT_DEMOTE_WITH_WRITEABLE_NC = 8604,
                /// <summary>
                /// The requested object has a non-unique identifier and cannot be retrieved.
                /// </summary>
                ERROR_DS_DUPLICATE_ID_FOUND = 8605,
                /// <summary>
                /// Insufficient attributes were given to create an object. This object may not exist because it may have been deleted and already garbage collected.
                /// </summary>
                ERROR_DS_INSUFFICIENT_ATTR_TO_CREATE_OBJECT = 8606,
                /// <summary>
                /// The group cannot be converted due to attribute restrictions on the requested group type.
                /// </summary>
                ERROR_DS_GROUP_CONVERSION_ERROR = 8607,
                /// <summary>
                /// Cross-domain move of non-empty basic application groups is not allowed.
                /// </summary>
                ERROR_DS_CANT_MOVE_APP_BASIC_GROUP = 8608,
                /// <summary>
                /// Cross-domain move of non-empty query based application groups is not allowed.
                /// </summary>
                ERROR_DS_CANT_MOVE_APP_QUERY_GROUP = 8609,
                /// <summary>
                /// The FSMO role ownership could not be verified because its directory partition has not replicated successfully with at least one replication partner.
                /// </summary>
                ERROR_DS_ROLE_NOT_VERIFIED = 8610,
                /// <summary>
                /// The target container for a redirection of a well known object container cannot already be a special container.
                /// </summary>
                ERROR_DS_WKO_CONTAINER_CANNOT_BE_SPECIAL = 8611,
                /// <summary>
                /// The Directory Service cannot perform the requested operation because a domain rename operation is in progress.
                /// </summary>
                ERROR_DS_DOMAIN_RENAME_IN_PROGRESS = 8612,
                /// <summary>
                /// The directory service detected a child partition below the requested partition name. The partition hierarchy must be created in a top down method.
                /// </summary>
                ERROR_DS_EXISTING_AD_CHILD_NC = 8613,
                /// <summary>
                /// The directory service cannot replicate with this server because the time since the last replication with this server has exceeded the tombstone lifetime.
                /// </summary>
                ERROR_DS_REPL_LIFETIME_EXCEEDED = 8614,
                /// <summary>
                /// The requested operation is not allowed on an object under the system container.
                /// </summary>
                ERROR_DS_DISALLOWED_IN_SYSTEM_CONTAINER = 8615,
                /// <summary>
                /// The LDAP servers network send queue has filled up because the client is not processing the results of its requests fast enough. No more requests will be processed until the client catches up. If the client does not catch up then it will be disconnected.
                /// </summary>
                ERROR_DS_LDAP_SEND_QUEUE_FULL = 8616,
                /// <summary>
                /// The scheduled replication did not take place because the system was too busy to execute the request within the schedule window. The replication queue is overloaded. Consider reducing the number of partners or decreasing the scheduled replication frequency.
                /// </summary>
                ERROR_DS_DRA_OUT_SCHEDULE_WINDOW = 8617,
                /// <summary>
                /// At this time, it cannot be determined if the branch replication policy is available on the hub domain controller. Please retry at a later time to account for replication latencies.
                /// </summary>
                ERROR_DS_POLICY_NOT_KNOWN = 8618,
                /// <summary>
                /// The site settings object for the specified site does not exist.
                /// </summary>
                ERROR_NO_SITE_SETTINGS_OBJECT = 8619,
                /// <summary>
                /// The local account store does not contain secret material for the specified account.
                /// </summary>
                ERROR_NO_SECRETS = 8620,
                /// <summary>
                /// Could not find a writable domain controller in the domain.
                /// </summary>
                ERROR_NO_WRITABLE_DC_FOUND = 8621,
                /// <summary>
                /// The server object for the domain controller does not exist.
                /// </summary>
                ERROR_DS_NO_SERVER_OBJECT = 8622,
                /// <summary>
                /// The NTDS Settings object for the domain controller does not exist.
                /// </summary>
                ERROR_DS_NO_NTDSA_OBJECT = 8623,
                /// <summary>
                /// The requested search operation is not supported for ASQ searches.
                /// </summary>
                ERROR_DS_NON_ASQ_SEARCH = 8624,
                /// <summary>
                /// A required audit event could not be generated for the operation.
                /// </summary>
                ERROR_DS_AUDIT_FAILURE = 8625,
                /// <summary>
                /// The search flags for the attribute are invalid. The subtree index bit is valid only on single valued attributes.
                /// </summary>
                ERROR_DS_INVALID_SEARCH_FLAG_SUBTREE = 8626,
                /// <summary>
                /// The search flags for the attribute are invalid. The tuple index bit is valid only on attributes of Unicode strings.
                /// </summary>
                ERROR_DS_INVALID_SEARCH_FLAG_TUPLE = 8627,
                /// <summary>
                /// The address books are nested too deeply. Failed to build the hierarchy table.
                /// </summary>
                ERROR_DS_HIERARCHY_TABLE_TOO_DEEP = 8628,
                /// <summary>
                /// The specified up-to-date-ness vector is corrupt.
                /// </summary>
                ERROR_DS_DRA_CORRUPT_UTD_VECTOR = 8629,
                /// <summary>
                /// The request to replicate secrets is denied.
                /// </summary>
                ERROR_DS_DRA_SECRETS_DENIED = 8630,
                /// <summary>
                /// Schema update failed: The MAPI identifier is reserved.
                /// </summary>
                ERROR_DS_RESERVED_MAPI_ID = 8631,
                /// <summary>
                /// Schema update failed: There are no MAPI identifiers available.
                /// </summary>
                ERROR_DS_MAPI_ID_NOT_AVAILABLE = 8632,
                /// <summary>
                /// The replication operation failed because the required attributes of the local krbtgt object are missing.
                /// </summary>
                ERROR_DS_DRA_MISSING_KRBTGT_SECRET = 8633,
                /// <summary>
                /// The domain name of the trusted domain already exists in the forest.
                /// </summary>
                ERROR_DS_DOMAIN_NAME_EXISTS_IN_FOREST = 8634,
                /// <summary>
                /// The flat name of the trusted domain already exists in the forest.
                /// </summary>
                ERROR_DS_FLAT_NAME_EXISTS_IN_FOREST = 8635,
                /// <summary>
                /// The User Principal Name (UPN) is invalid.
                /// </summary>
                ERROR_INVALID_USER_PRINCIPAL_NAME = 8636,
                /// <summary>
                /// OID mapped groups cannot have members.
                /// </summary>
                ERROR_DS_OID_MAPPED_GROUP_CANT_HAVE_MEMBERS = 8637,
                /// <summary>
                /// The specified OID cannot be found.
                /// </summary>
                ERROR_DS_OID_NOT_FOUND = 8638,
                /// <summary>
                /// The replication operation failed because the target object referred by a link value is recycled.
                /// </summary>
                ERROR_DS_DRA_RECYCLED_TARGET = 8639,
                /// <summary>
                /// The redirect operation failed because the target object is in a NC different from the domain NC of the current domain controller.
                /// </summary>
                ERROR_DS_DISALLOWED_NC_REDIRECT = 8640,
                /// <summary>
                /// The functional level of the AD LDS configuration set cannot be lowered to the requested value.
                /// </summary>
                ERROR_DS_HIGH_ADLDS_FFL = 8641,
                /// <summary>
                /// The functional level of the domain (or forest) cannot be lowered to the requested value.
                /// </summary>
                ERROR_DS_HIGH_DSA_VERSION = 8642,
                /// <summary>
                /// The functional level of the AD LDS configuration set cannot be raised to the requested value, because there exist one or more ADLDS instances that are at a lower incompatible functional level.
                /// </summary>
                ERROR_DS_LOW_ADLDS_FFL = 8643,
                /// <summary>
                /// The domain join cannot be completed because the SID of the domain you attempted to join was identical to the SID of this machine. This is a symptom of an improperly cloned operating system install.  You should run sysprep on this machine in order to generate a new machine SID. Please see http://go.microsoft.com/fwlink/p/?linkid=168895 for more information.
                /// </summary>
                ERROR_DOMAIN_SID_SAME_AS_LOCAL_WORKSTATION = 8644,
                /// <summary>
                /// The undelete operation failed because the Sam Account Name or Additional Sam Account Name of the object being undeleted conflicts with an existing live object.
                /// </summary>
                ERROR_DS_UNDELETE_SAM_VALIDATION_FAILED = 8645,
                /// <summary>
                /// The system is not authoritative for the specified account and therefore cannot complete the operation. Please retry the operation using the provider associated with this account. If this is an online provider please use the provider's online site.
                /// </summary>
                ERROR_INCORRECT_ACCOUNT_TYPE = 8646,/// <summary>
                                                    /// DNS server unable to interpret format.
                                                    /// </summary>
                DNS_ERROR_RCODE_FORMAT_ERROR = 9001,
                /// <summary>
                /// DNS server failure.
                /// </summary>
                DNS_ERROR_RCODE_SERVER_FAILURE = 9002,
                /// <summary>
                /// DNS name does not exist.
                /// </summary>
                DNS_ERROR_RCODE_NAME_ERROR = 9003,
                /// <summary>
                /// DNS request not supported by name server.
                /// </summary>
                DNS_ERROR_RCODE_NOT_IMPLEMENTED = 9004,
                /// <summary>
                /// DNS operation refused.
                /// </summary>
                DNS_ERROR_RCODE_REFUSED = 9005,
                /// <summary>
                /// DNS name that ought not exist, does exist.
                /// </summary>
                DNS_ERROR_RCODE_YXDOMAIN = 9006,
                /// <summary>
                /// DNS RR set that ought not exist, does exist.
                /// </summary>
                DNS_ERROR_RCODE_YXRRSET = 9007,
                /// <summary>
                /// DNS RR set that ought to exist, does not exist.
                /// </summary>
                DNS_ERROR_RCODE_NXRRSET = 9008,
                /// <summary>
                /// DNS server not authoritative for zone.
                /// </summary>
                DNS_ERROR_RCODE_NOTAUTH = 9009,
                /// <summary>
                /// DNS name in update or prereq is not in zone.
                /// </summary>
                DNS_ERROR_RCODE_NOTZONE = 9010,
                /// <summary>
                /// DNS signature failed to verify.
                /// </summary>
                DNS_ERROR_RCODE_BADSIG = 9016,
                /// <summary>
                /// DNS bad key.
                /// </summary>
                DNS_ERROR_RCODE_BADKEY = 9017,
                /// <summary>
                /// DNS signature validity expired.
                /// </summary>
                DNS_ERROR_RCODE_BADTIME = 9018,
                /// <summary>
                /// Only the DNS server acting as the key master for the zone may perform this operation.
                /// </summary>
                DNS_ERROR_KEYMASTER_REQUIRED = 9101,
                /// <summary>
                /// This operation is not allowed on a zone that is signed or has signing keys.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_SIGNED_ZONE = 9102,
                /// <summary>
                /// NSEC3 is not compatible with the RSA-SHA-1 algorithm. Choose a different algorithm or use NSEC. This value was also named DNS_ERROR_INVALID_NSEC3_PARAMETERS
                /// </summary>
                DNS_ERROR_NSEC3_INCOMPATIBLE_WITH_RSA_SHA1 = 9103,
                /// <summary>
                /// The zone does not have enough signing keys. There must be at least one key signing key (KSK) and at least one zone signing key (ZSK).
                /// </summary>
                DNS_ERROR_NOT_ENOUGH_SIGNING_KEY_DESCRIPTORS = 9104,
                /// <summary>
                /// The specified algorithm is not supported.
                /// </summary>
                DNS_ERROR_UNSUPPORTED_ALGORITHM = 9105,
                /// <summary>
                /// The specified key size is not supported.
                /// </summary>
                DNS_ERROR_INVALID_KEY_SIZE = 9106,
                /// <summary>
                /// One or more of the signing keys for a zone are not accessible to the DNS server. Zone signing will not be operational until this error is resolved.
                /// </summary>
                DNS_ERROR_SIGNING_KEY_NOT_ACCESSIBLE = 9107,
                /// <summary>
                /// The specified key storage provider does not support DPAPI++ data protection. Zone signing will not be operational until this error is resolved.
                /// </summary>
                DNS_ERROR_KSP_DOES_NOT_SUPPORT_PROTECTION = 9108,
                /// <summary>
                /// An unexpected DPAPI++ error was encountered. Zone signing will not be operational until this error is resolved.
                /// </summary>
                DNS_ERROR_UNEXPECTED_DATA_PROTECTION_ERROR = 9109,
                /// <summary>
                /// An unexpected crypto error was encountered. Zone signing may not be operational until this error is resolved.
                /// </summary>
                DNS_ERROR_UNEXPECTED_CNG_ERROR = 9110,
                /// <summary>
                /// The DNS server encountered a signing key with an unknown version. Zone signing will not be operational until this error is resolved.
                /// </summary>
                DNS_ERROR_UNKNOWN_SIGNING_PARAMETER_VERSION = 9111,
                /// <summary>
                /// The specified key service provider cannot be opened by the DNS server.
                /// </summary>
                DNS_ERROR_KSP_NOT_ACCESSIBLE = 9112,
                /// <summary>
                /// The DNS server cannot accept any more signing keys with the specified algorithm and KSK flag value for this zone.
                /// </summary>
                DNS_ERROR_TOO_MANY_SKDS = 9113,
                /// <summary>
                /// The specified rollover period is invalid.
                /// </summary>
                DNS_ERROR_INVALID_ROLLOVER_PERIOD = 9114,
                /// <summary>
                /// The specified initial rollover offset is invalid.
                /// </summary>
                DNS_ERROR_INVALID_INITIAL_ROLLOVER_OFFSET = 9115,
                /// <summary>
                /// The specified signing key is already in process of rolling over keys.
                /// </summary>
                DNS_ERROR_ROLLOVER_IN_PROGRESS = 9116,
                /// <summary>
                /// The specified signing key does not have a standby key to revoke.
                /// </summary>
                DNS_ERROR_STANDBY_KEY_NOT_PRESENT = 9117,
                /// <summary>
                /// This operation is not allowed on a zone signing key (ZSK).
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_ZSK = 9118,
                /// <summary>
                /// This operation is not allowed on an active signing key.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_ACTIVE_SKD = 9119,
                /// <summary>
                /// The specified signing key is already queued for rollover.
                /// </summary>
                DNS_ERROR_ROLLOVER_ALREADY_QUEUED = 9120,
                /// <summary>
                /// This operation is not allowed on an unsigned zone.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_UNSIGNED_ZONE = 9121,
                /// <summary>
                /// This operation could not be completed because the DNS server listed as the current key master for this zone is down or misconfigured. Resolve the problem on the current key master for this zone or use another DNS server to seize the key master role.
                /// </summary>
                DNS_ERROR_BAD_KEYMASTER = 9122,
                /// <summary>
                /// The specified signature validity period is invalid.
                /// </summary>
                DNS_ERROR_INVALID_SIGNATURE_VALIDITY_PERIOD = 9123,
                /// <summary>
                /// The specified NSEC3 iteration count is higher than allowed by the minimum key length used in the zone.
                /// </summary>
                DNS_ERROR_INVALID_NSEC3_ITERATION_COUNT = 9124,
                /// <summary>
                /// This operation could not be completed because the DNS server has been configured with DNSSEC features disabled. Enable DNSSEC on the DNS server.
                /// </summary>
                DNS_ERROR_DNSSEC_IS_DISABLED = 9125,
                /// <summary>
                /// This operation could not be completed because the XML stream received is empty or syntactically invalid.
                /// </summary>
                DNS_ERROR_INVALID_XML = 9126,
                /// <summary>
                /// This operation completed, but no trust anchors were added because all of the trust anchors received were either invalid, unsupported, expired, or would not become valid in less than 30 days.
                /// </summary>
                DNS_ERROR_NO_VALID_TRUST_ANCHORS = 9127,
                /// <summary>
                /// The specified signing key is not waiting for parental DS update.
                /// </summary>
                DNS_ERROR_ROLLOVER_NOT_POKEABLE = 9128,
                /// <summary>
                /// Hash collision detected during NSEC3 signing. Specify a different user-provided salt, or use a randomly generated salt, and attempt to sign the zone again.
                /// </summary>
                DNS_ERROR_NSEC3_NAME_COLLISION = 9129,
                /// <summary>
                /// NSEC is not compatible with the NSEC3-RSA-SHA-1 algorithm. Choose a different algorithm or use NSEC3.
                /// </summary>
                DNS_ERROR_NSEC_INCOMPATIBLE_WITH_NSEC3_RSA_SHA1 = 9130,
                /// <summary>
                /// No records found for given DNS query.
                /// </summary>
                DNS_INFO_NO_RECORDS = 9501,
                /// <summary>
                /// Bad DNS packet.
                /// </summary>
                DNS_ERROR_BAD_PACKET = 9502,
                /// <summary>
                /// No DNS packet.
                /// </summary>
                DNS_ERROR_NO_PACKET = 9503,
                /// <summary>
                /// DNS error, check rcode.
                /// </summary>
                DNS_ERROR_RCODE = 9504,
                /// <summary>
                /// Unsecured DNS packet.
                /// </summary>
                DNS_ERROR_UNSECURE_PACKET = 9505,
                /// <summary>
                /// DNS query request is pending.
                /// </summary>
                DNS_REQUEST_PENDING = 9506,
                /// <summary>
                /// Invalid DNS type.
                /// </summary>
                DNS_ERROR_INVALID_TYPE = 9551,
                /// <summary>
                /// Invalid IP address.
                /// </summary>
                DNS_ERROR_INVALID_IP_ADDRESS = 9552,
                /// <summary>
                /// Invalid property.
                /// </summary>
                DNS_ERROR_INVALID_PROPERTY = 9553,
                /// <summary>
                /// Try DNS operation again later.
                /// </summary>
                DNS_ERROR_TRY_AGAIN_LATER = 9554,
                /// <summary>
                /// Record for given name and type is not unique.
                /// </summary>
                DNS_ERROR_NOT_UNIQUE = 9555,
                /// <summary>
                /// DNS name does not comply with RFC specifications.
                /// </summary>
                DNS_ERROR_NON_RFC_NAME = 9556,
                /// <summary>
                /// DNS name is a fully-qualified DNS name.
                /// </summary>
                DNS_STATUS_FQDN = 9557,
                /// <summary>
                /// DNS name is dotted (multi-label).
                /// </summary>
                DNS_STATUS_DOTTED_NAME = 9558,
                /// <summary>
                /// DNS name is a single-part name.
                /// </summary>
                DNS_STATUS_SINGLE_PART_NAME = 9559,
                /// <summary>
                /// DNS name contains an invalid character.
                /// </summary>
                DNS_ERROR_INVALID_NAME_CHAR = 9560,
                /// <summary>
                /// DNS name is entirely numeric.
                /// </summary>
                DNS_ERROR_NUMERIC_NAME = 9561,
                /// <summary>
                /// The operation requested is not permitted on a DNS root server.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER = 9562,
                /// <summary>
                /// The record could not be created because this part of the DNS namespace has been delegated to another server.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_UNDER_DELEGATION = 9563,
                /// <summary>
                /// The DNS server could not find a set of root hints.
                /// </summary>
                DNS_ERROR_CANNOT_FIND_ROOT_HINTS = 9564,
                /// <summary>
                /// The DNS server found root hints but they were not consistent across all adapters.
                /// </summary>
                DNS_ERROR_INCONSISTENT_ROOT_HINTS = 9565,
                /// <summary>
                /// The specified value is too small for this parameter.
                /// </summary>
                DNS_ERROR_DWORD_VALUE_TOO_SMALL = 9566,
                /// <summary>
                /// The specified value is too large for this parameter.
                /// </summary>
                DNS_ERROR_DWORD_VALUE_TOO_LARGE = 9567,
                /// <summary>
                /// This operation is not allowed while the DNS server is loading zones in the background. Please try again later.
                /// </summary>
                DNS_ERROR_BACKGROUND_LOADING = 9568,
                /// <summary>
                /// The operation requested is not permitted on against a DNS server running on a read-only DC.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_ON_RODC = 9569,
                /// <summary>
                /// No data is allowed to exist underneath a DNAME record.
                /// </summary>
                DNS_ERROR_NOT_ALLOWED_UNDER_DNAME = 9570,
                /// <summary>
                /// This operation requires credentials delegation.
                /// </summary>
                DNS_ERROR_DELEGATION_REQUIRED = 9571,
                /// <summary>
                /// Name resolution policy table has been corrupted. DNS resolution will fail until it is fixed. Contact your network administrator.
                /// </summary>
                DNS_ERROR_INVALID_POLICY_TABLE = 9572,
                /// <summary>
                /// DNS zone does not exist.
                /// </summary>
                DNS_ERROR_ZONE_DOES_NOT_EXIST = 9601,
                /// <summary>
                /// DNS zone information not available.
                /// </summary>
                DNS_ERROR_NO_ZONE_INFO = 9602,
                /// <summary>
                /// Invalid operation for DNS zone.
                /// </summary>
                DNS_ERROR_INVALID_ZONE_OPERATION = 9603,
                /// <summary>
                /// Invalid DNS zone configuration.
                /// </summary>
                DNS_ERROR_ZONE_CONFIGURATION_ERROR = 9604,
                /// <summary>
                /// DNS zone has no start of authority (SOA) record.
                /// </summary>
                DNS_ERROR_ZONE_HAS_NO_SOA_RECORD = 9605,
                /// <summary>
                /// DNS zone has no Name Server (NS) record.
                /// </summary>
                DNS_ERROR_ZONE_HAS_NO_NS_RECORDS = 9606,
                /// <summary>
                /// DNS zone is locked.
                /// </summary>
                DNS_ERROR_ZONE_LOCKED = 9607,
                /// <summary>
                /// DNS zone creation failed.
                /// </summary>
                DNS_ERROR_ZONE_CREATION_FAILED = 9608,
                /// <summary>
                /// DNS zone already exists.
                /// </summary>
                DNS_ERROR_ZONE_ALREADY_EXISTS = 9609,
                /// <summary>
                /// DNS automatic zone already exists.
                /// </summary>
                DNS_ERROR_AUTOZONE_ALREADY_EXISTS = 9610,
                /// <summary>
                /// Invalid DNS zone type.
                /// </summary>
                DNS_ERROR_INVALID_ZONE_TYPE = 9611,
                /// <summary>
                /// Secondary DNS zone requires master IP address.
                /// </summary>
                DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP = 9612,
                /// <summary>
                /// DNS zone not secondary.
                /// </summary>
                DNS_ERROR_ZONE_NOT_SECONDARY = 9613,
                /// <summary>
                /// Need secondary IP address.
                /// </summary>
                DNS_ERROR_NEED_SECONDARY_ADDRESSES = 9614,
                /// <summary>
                /// WINS initialization failed.
                /// </summary>
                DNS_ERROR_WINS_INIT_FAILED = 9615,
                /// <summary>
                /// Need WINS servers.
                /// </summary>
                DNS_ERROR_NEED_WINS_SERVERS = 9616,
                /// <summary>
                /// NBTSTAT initialization call failed.
                /// </summary>
                DNS_ERROR_NBSTAT_INIT_FAILED = 9617,
                /// <summary>
                /// Invalid delete of start of authority (SOA).
                /// </summary>
                DNS_ERROR_SOA_DELETE_INVALID = 9618,
                /// <summary>
                /// A conditional forwarding zone already exists for that name.
                /// </summary>
                DNS_ERROR_FORWARDER_ALREADY_EXISTS = 9619,
                /// <summary>
                /// This zone must be configured with one or more master DNS server IP addresses.
                /// </summary>
                DNS_ERROR_ZONE_REQUIRES_MASTER_IP = 9620,
                /// <summary>
                /// The operation cannot be performed because this zone is shut down.
                /// </summary>
                DNS_ERROR_ZONE_IS_SHUTDOWN = 9621,
                /// <summary>
                /// This operation cannot be performed because the zone is currently being signed. Please try again later.
                /// </summary>
                DNS_ERROR_ZONE_LOCKED_FOR_SIGNING = 9622,
                /// <summary>
                /// Primary DNS zone requires datafile.
                /// </summary>
                DNS_ERROR_PRIMARY_REQUIRES_DATAFILE = 9651,
                /// <summary>
                /// Invalid datafile name for DNS zone.
                /// </summary>
                DNS_ERROR_INVALID_DATAFILE_NAME = 9652,
                /// <summary>
                /// Failed to open datafile for DNS zone.
                /// </summary>
                DNS_ERROR_DATAFILE_OPEN_FAILURE = 9653,
                /// <summary>
                /// Failed to write datafile for DNS zone.
                /// </summary>
                DNS_ERROR_FILE_WRITEBACK_FAILED = 9654,
                /// <summary>
                /// Failure while reading datafile for DNS zone.
                /// </summary>
                DNS_ERROR_DATAFILE_PARSING = 9655,
                /// <summary>
                /// DNS record does not exist.
                /// </summary>
                DNS_ERROR_RECORD_DOES_NOT_EXIST = 9701,
                /// <summary>
                /// DNS record format error.
                /// </summary>
                DNS_ERROR_RECORD_FORMAT = 9702,
                /// <summary>
                /// Node creation failure in DNS.
                /// </summary>
                DNS_ERROR_NODE_CREATION_FAILED = 9703,
                /// <summary>
                /// Unknown DNS record type.
                /// </summary>
                DNS_ERROR_UNKNOWN_RECORD_TYPE = 9704,
                /// <summary>
                /// DNS record timed out.
                /// </summary>
                DNS_ERROR_RECORD_TIMED_OUT = 9705,
                /// <summary>
                /// Name not in DNS zone.
                /// </summary>
                DNS_ERROR_NAME_NOT_IN_ZONE = 9706,
                /// <summary>
                /// CNAME loop detected.
                /// </summary>
                DNS_ERROR_CNAME_LOOP = 9707,
                /// <summary>
                /// Node is a CNAME DNS record.
                /// </summary>
                DNS_ERROR_NODE_IS_CNAME = 9708,
                /// <summary>
                /// A CNAME record already exists for given name.
                /// </summary>
                DNS_ERROR_CNAME_COLLISION = 9709,
                /// <summary>
                /// Record only at DNS zone root.
                /// </summary>
                DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT = 9710,
                /// <summary>
                /// DNS record already exists.
                /// </summary>
                DNS_ERROR_RECORD_ALREADY_EXISTS = 9711,
                /// <summary>
                /// Secondary DNS zone data error.
                /// </summary>
                DNS_ERROR_SECONDARY_DATA = 9712,
                /// <summary>
                /// Could not create DNS cache data.
                /// </summary>
                DNS_ERROR_NO_CREATE_CACHE_DATA = 9713,
                /// <summary>
                /// DNS name does not exist.
                /// </summary>
                DNS_ERROR_NAME_DOES_NOT_EXIST = 9714,
                /// <summary>
                /// Could not create pointer (PTR) record.
                /// </summary>
                DNS_WARNING_PTR_CREATE_FAILED = 9715,
                /// <summary>
                /// DNS domain was undeleted.
                /// </summary>
                DNS_WARNING_DOMAIN_UNDELETED = 9716,
                /// <summary>
                /// The directory service is unavailable.
                /// </summary>
                DNS_ERROR_DS_UNAVAILABLE = 9717,
                /// <summary>
                /// DNS zone already exists in the directory service.
                /// </summary>
                DNS_ERROR_DS_ZONE_ALREADY_EXISTS = 9718,
                /// <summary>
                /// DNS server not creating or reading the boot file for the directory service integrated DNS zone.
                /// </summary>
                DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE = 9719,
                /// <summary>
                /// Node is a DNAME DNS record.
                /// </summary>
                DNS_ERROR_NODE_IS_DNAME = 9720,
                /// <summary>
                /// A DNAME record already exists for given name.
                /// </summary>
                DNS_ERROR_DNAME_COLLISION = 9721,
                /// <summary>
                /// An alias loop has been detected with either CNAME or DNAME records.
                /// </summary>
                DNS_ERROR_ALIAS_LOOP = 9722,
                /// <summary>
                /// DNS AXFR (zone transfer) complete.
                /// </summary>
                DNS_INFO_AXFR_COMPLETE = 9751,
                /// <summary>
                /// DNS zone transfer failed.
                /// </summary>
                DNS_ERROR_AXFR = 9752,
                /// <summary>
                /// Added local WINS server.
                /// </summary>
                DNS_INFO_ADDED_LOCAL_WINS = 9753,
                /// <summary>
                /// Secure update call needs to continue update request.
                /// </summary>
                DNS_STATUS_CONTINUE_NEEDED = 9801,
                /// <summary>
                /// TCP/IP network protocol not installed.
                /// </summary>
                DNS_ERROR_NO_TCPIP = 9851,
                /// <summary>
                /// No DNS servers configured for local system.
                /// </summary>
                DNS_ERROR_NO_DNS_SERVERS = 9852,
                /// <summary>
                /// The specified directory partition does not exist.
                /// </summary>
                DNS_ERROR_DP_DOES_NOT_EXIST = 9901,
                /// <summary>
                /// The specified directory partition already exists.
                /// </summary>
                DNS_ERROR_DP_ALREADY_EXISTS = 9902,
                /// <summary>
                /// This DNS server is not enlisted in the specified directory partition.
                /// </summary>
                DNS_ERROR_DP_NOT_ENLISTED = 9903,
                /// <summary>
                /// This DNS server is already enlisted in the specified directory partition.
                /// </summary>
                DNS_ERROR_DP_ALREADY_ENLISTED = 9904,
                /// <summary>
                /// The directory partition is not available at this time. Please wait a few minutes and try again.
                /// </summary>
                DNS_ERROR_DP_NOT_AVAILABLE = 9905,
                /// <summary>
                /// The operation failed because the domain naming master FSMO role could not be reached. The domain controller holding the domain naming master FSMO role is down or unable to service the request or is not running Windows Server 2003 or later.
                /// </summary>
                DNS_ERROR_DP_FSMO_ERROR = 9906,
                /// <summary>
                /// A blocking operation was interrupted by a call to WSACancelBlockingCall.
                /// </summary>
                WSAEINTR = 10004,
                /// <summary>
                /// The file handle supplied is not valid.
                /// </summary>
                WSAEBADF = 10009,
                /// <summary>
                /// An attempt was made to access a socket in a way forbidden by its access permissions.
                /// </summary>
                WSAEACCES = 10013,
                /// <summary>
                /// The system detected an invalid pointer address in attempting to use a pointer argument in a call.
                /// </summary>
                WSAEFAULT = 10014,
                /// <summary>
                /// An invalid argument was supplied.
                /// </summary>
                WSAEINVAL = 10022,
                /// <summary>
                /// Too many open sockets.
                /// </summary>
                WSAEMFILE = 10024,
                /// <summary>
                /// A non-blocking socket operation could not be completed immediately.
                /// </summary>
                WSAEWOULDBLOCK = 10035,
                /// <summary>
                /// A blocking operation is currently executing.
                /// </summary>
                WSAEINPROGRESS = 10036,
                /// <summary>
                /// An operation was attempted on a non-blocking socket that already had an operation in progress.
                /// </summary>
                WSAEALREADY = 10037,
                /// <summary>
                /// An operation was attempted on something that is not a socket.
                /// </summary>
                WSAENOTSOCK = 10038,
                /// <summary>
                /// A required address was omitted from an operation on a socket.
                /// </summary>
                WSAEDESTADDRREQ = 10039,
                /// <summary>
                /// A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself.
                /// </summary>
                WSAEMSGSIZE = 10040,
                /// <summary>
                /// A protocol was specified in the socket function call that does not support the semantics of the socket type requested.
                /// </summary>
                WSAEPROTOTYPE = 10041,
                /// <summary>
                /// An unknown, invalid, or unsupported option or level was specified in a getsockopt or setsockopt call.
                /// </summary>
                WSAENOPROTOOPT = 10042,
                /// <summary>
                /// The requested protocol has not been configured into the system, or no implementation for it exists.
                /// </summary>
                WSAEPROTONOSUPPORT = 10043,
                /// <summary>
                /// The support for the specified socket type does not exist in this address family.
                /// </summary>
                WSAESOCKTNOSUPPORT = 10044,
                /// <summary>
                /// The attempted operation is not supported for the type of object referenced.
                /// </summary>
                WSAEOPNOTSUPP = 10045,
                /// <summary>
                /// The protocol family has not been configured into the system or no implementation for it exists.
                /// </summary>
                WSAEPFNOSUPPORT = 10046,
                /// <summary>
                /// An address incompatible with the requested protocol was used.
                /// </summary>
                WSAEAFNOSUPPORT = 10047,
                /// <summary>
                /// Only one usage of each socket address (protocol/network address/port) is normally permitted.
                /// </summary>
                WSAEADDRINUSE = 10048,
                /// <summary>
                /// The requested address is not valid in its context.
                /// </summary>
                WSAEADDRNOTAVAIL = 10049,
                /// <summary>
                /// A socket operation encountered a dead network.
                /// </summary>
                WSAENETDOWN = 10050,
                /// <summary>
                /// A socket operation was attempted to an unreachable network.
                /// </summary>
                WSAENETUNREACH = 10051,
                /// <summary>
                /// The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress.
                /// </summary>
                WSAENETRESET = 10052,
                /// <summary>
                /// An established connection was aborted by the software in your host machine.
                /// </summary>
                WSAECONNABORTED = 10053,
                /// <summary>
                /// An existing connection was forcibly closed by the remote host.
                /// </summary>
                WSAECONNRESET = 10054,
                /// <summary>
                /// An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.
                /// </summary>
                WSAENOBUFS = 10055,
                /// <summary>
                /// A connect request was made on an already connected socket.
                /// </summary>
                WSAEISCONN = 10056,
                /// <summary>
                /// A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied.
                /// </summary>
                WSAENOTCONN = 10057,
                /// <summary>
                /// A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call.
                /// </summary>
                WSAESHUTDOWN = 10058,
                /// <summary>
                /// Too many references to some kernel object.
                /// </summary>
                WSAETOOMANYREFS = 10059,
                /// <summary>
                /// A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.
                /// </summary>
                WSAETIMEDOUT = 10060,
                /// <summary>
                /// No connection could be made because the target machine actively refused it.
                /// </summary>
                WSAECONNREFUSED = 10061,
                /// <summary>
                /// Cannot translate name.
                /// </summary>
                WSAELOOP = 10062,
                /// <summary>
                /// Name component or name was too long.
                /// </summary>
                WSAENAMETOOLONG = 10063,
                /// <summary>
                /// A socket operation failed because the destination host was down.
                /// </summary>
                WSAEHOSTDOWN = 10064,
                /// <summary>
                /// A socket operation was attempted to an unreachable host.
                /// </summary>
                WSAEHOSTUNREACH = 10065,
                /// <summary>
                /// Cannot remove a directory that is not empty.
                /// </summary>
                WSAENOTEMPTY = 10066,
                /// <summary>
                /// A Windows Sockets implementation may have a limit on the number of applications that may use it simultaneously.
                /// </summary>
                WSAEPROCLIM = 10067,
                /// <summary>
                /// Ran out of quota.
                /// </summary>
                WSAEUSERS = 10068,
                /// <summary>
                /// Ran out of disk quota.
                /// </summary>
                WSAEDQUOT = 10069,
                /// <summary>
                /// File handle reference is no longer available.
                /// </summary>
                WSAESTALE = 10070,
                /// <summary>
                /// Item is not available locally.
                /// </summary>
                WSAEREMOTE = 10071,
                /// <summary>
                /// WSAStartup cannot function at this time because the underlying system it uses to provide network services is currently unavailable.
                /// </summary>
                WSASYSNOTREADY = 10091,
                /// <summary>
                /// The Windows Sockets version requested is not supported.
                /// </summary>
                WSAVERNOTSUPPORTED = 10092,
                /// <summary>
                /// Either the application has not called WSAStartup, or WSAStartup failed.
                /// </summary>
                WSANOTINITIALISED = 10093,
                /// <summary>
                /// Returned by WSARecv or WSARecvFrom to indicate the remote party has initiated a graceful shutdown sequence.
                /// </summary>
                WSAEDISCON = 10101,
                /// <summary>
                /// No more results can be returned by WSALookupServiceNext.
                /// </summary>
                WSAENOMORE = 10102,
                /// <summary>
                /// A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.
                /// </summary>
                WSAECANCELLED = 10103,
                /// <summary>
                /// The procedure call table is invalid.
                /// </summary>
                WSAEINVALIDPROCTABLE = 10104,
                /// <summary>
                /// The requested service provider is invalid.
                /// </summary>
                WSAEINVALIDPROVIDER = 10105,
                /// <summary>
                /// The requested service provider could not be loaded or initialized.
                /// </summary>
                WSAEPROVIDERFAILEDINIT = 10106,
                /// <summary>
                /// A system call has failed.
                /// </summary>
                WSASYSCALLFAILURE = 10107,
                /// <summary>
                /// No such service is known. The service cannot be found in the specified name space.
                /// </summary>
                WSASERVICE_NOT_FOUND = 10108,
                /// <summary>
                /// The specified class was not found.
                /// </summary>
                WSATYPE_NOT_FOUND = 10109,
                /// <summary>
                /// No more results can be returned by WSALookupServiceNext.
                /// </summary>
                WSA_E_NO_MORE = 10110,
                /// <summary>
                /// A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.
                /// </summary>
                WSA_E_CANCELLED = 10111,
                /// <summary>
                /// A database query failed because it was actively refused.
                /// </summary>
                WSAEREFUSED = 10112,
                /// <summary>
                /// No such host is known.
                /// </summary>
                WSAHOST_NOT_FOUND = 11001,
                /// <summary>
                /// This is usually a temporary error during hostname resolution and means that the local server did not receive a response from an authoritative server.
                /// </summary>
                WSATRY_AGAIN = 11002,
                /// <summary>
                /// A non-recoverable error occurred during a database lookup.
                /// </summary>
                WSANO_RECOVERY = 11003,
                /// <summary>
                /// The requested name is valid, but no data of the requested type was found.
                /// </summary>
                WSANO_DATA = 11004,
                /// <summary>
                /// At least one reserve has arrived.
                /// </summary>
                WSA_QOS_RECEIVERS = 11005,
                /// <summary>
                /// At least one path has arrived.
                /// </summary>
                WSA_QOS_SENDERS = 11006,
                /// <summary>
                /// There are no senders.
                /// </summary>
                WSA_QOS_NO_SENDERS = 11007,
                /// <summary>
                /// There are no receivers.
                /// </summary>
                WSA_QOS_NO_RECEIVERS = 11008,
                /// <summary>
                /// Reserve has been confirmed.
                /// </summary>
                WSA_QOS_REQUEST_CONFIRMED = 11009,
                /// <summary>
                /// Error due to lack of resources.
                /// </summary>
                WSA_QOS_ADMISSION_FAILURE = 11010,
                /// <summary>
                /// Rejected for administrative reasons - bad credentials.
                /// </summary>
                WSA_QOS_POLICY_FAILURE = 11011,
                /// <summary>
                /// Unknown or conflicting style.
                /// </summary>
                WSA_QOS_BAD_STYLE = 11012,
                /// <summary>
                /// Problem with some part of the filterspec or providerspecific buffer in general.
                /// </summary>
                WSA_QOS_BAD_OBJECT = 11013,
                /// <summary>
                /// Problem with some part of the flowspec.
                /// </summary>
                WSA_QOS_TRAFFIC_CTRL_ERROR = 11014,
                /// <summary>
                /// General QOS error.
                /// </summary>
                WSA_QOS_GENERIC_ERROR = 11015,
                /// <summary>
                /// An invalid or unrecognized service type was found in the flowspec.
                /// </summary>
                WSA_QOS_ESERVICETYPE = 11016,
                /// <summary>
                /// An invalid or inconsistent flowspec was found in the QOS structure.
                /// </summary>
                WSA_QOS_EFLOWSPEC = 11017,
                /// <summary>
                /// Invalid QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_EPROVSPECBUF = 11018,
                /// <summary>
                /// An invalid QOS filter style was used.
                /// </summary>
                WSA_QOS_EFILTERSTYLE = 11019,
                /// <summary>
                /// An invalid QOS filter type was used.
                /// </summary>
                WSA_QOS_EFILTERTYPE = 11020,
                /// <summary>
                /// An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.
                /// </summary>
                WSA_QOS_EFILTERCOUNT = 11021,
                /// <summary>
                /// An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_EOBJLENGTH = 11022,
                /// <summary>
                /// An incorrect number of flow descriptors was specified in the QOS structure.
                /// </summary>
                WSA_QOS_EFLOWCOUNT = 11023,
                /// <summary>
                /// An unrecognized object was found in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_EUNKOWNPSOBJ = 11024,
                /// <summary>
                /// An invalid policy object was found in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_EPOLICYOBJ = 11025,
                /// <summary>
                /// An invalid QOS flow descriptor was found in the flow descriptor list.
                /// </summary>
                WSA_QOS_EFLOWDESC = 11026,
                /// <summary>
                /// An invalid or inconsistent flowspec was found in the QOS provider specific buffer.
                /// </summary>
                WSA_QOS_EPSFLOWSPEC = 11027,
                /// <summary>
                /// An invalid FILTERSPEC was found in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_EPSFILTERSPEC = 11028,
                /// <summary>
                /// An invalid shape discard mode object was found in the QOS provider specific buffer.
                /// </summary>
                WSA_QOS_ESDMODEOBJ = 11029,
                /// <summary>
                /// An invalid shaping rate object was found in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_ESHAPERATEOBJ = 11030,
                /// <summary>
                /// A reserved policy element was found in the QOS provider-specific buffer.
                /// </summary>
                WSA_QOS_RESERVED_PETYPE = 11031,
                /// <summary>
                /// No such host is known securely.
                /// </summary>
                WSA_SECURE_HOST_NOT_FOUND = 11032,
                /// <summary>
                /// Name based IPSEC policy could not be added.
                /// </summary>
                WSA_IPSEC_NAME_POLICY_ERROR = 11033,
                /// <summary>
                /// The FTP operation was not completed because the session was aborted.
                /// </summary>
                ERROR_FTP_DROPPED = 12111,
                /// <summary>
                /// Passive mode is not available on the server.
                /// </summary>
                ERROR_FTP_NO_PASSIVE_MODE = 12112,
                /// <summary>
                /// The requested operation cannot be made on the FTP session handle because an operation is already in progress.
                /// </summary>
                ERROR_FTP_TRANSFER_IN_PROGRESS = 12110,
                /// <summary>
                /// The requested attribute could not be located.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_ATTRIBUTE_NOT_FOUND = 12137,
                /// <summary>
                /// An error was detected while receiving data from the Gopher server.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_DATA_ERROR = 12132,
                /// <summary>
                /// The end of the data has been reached.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_END_OF_DATA = 12133,
                /// <summary>
                /// The type of the locator is not correct for this operation.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_INCORRECT_LOCATOR_TYPE = 12135,
                /// <summary>
                /// The supplied locator is not valid.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_INVALID_LOCATOR = 12134,
                /// <summary>
                /// The request must be made for a file locator.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_NOT_FILE = 12131,
                /// <summary>
                /// The requested operation can be made only against a Gopher+ server, or with a locator that specifies a Gopher+ operation.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_NOT_GOPHER_PLUS = 12136,
                /// <summary>
                /// An error was detected while parsing data returned from the Gopher server.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_PROTOCOL_ERROR = 12130,
                /// <summary>
                /// The locator type is unknown.\nNote  Windows XP and Windows Server 2003 R2 and earlier only.
                /// </summary>
                ERROR_GOPHER_UNKNOWN_LOCATOR = 12138,
                /// <summary>
                /// The HTTP cookie was declined by the server.
                /// </summary>
                ERROR_HTTP_COOKIE_DECLINED = 12162,
                /// <summary>
                /// The HTTP cookie requires confirmation.\nNote  Windows Vista and Windows Server 2008 and earlier only.
                /// </summary>
                ERROR_HTTP_COOKIE_NEEDS_CONFIRMATION = 12161,
                /// <summary>
                /// The server did not return any headers.
                /// </summary>
                ERROR_HTTP_DOWNLEVEL_SERVER = 12151,
                /// <summary>
                /// The header could not be added because it already exists.
                /// </summary>
                ERROR_HTTP_HEADER_ALREADY_EXISTS = 12155,
                /// <summary>
                /// The requested header could not be located.
                /// </summary>
                ERROR_HTTP_HEADER_NOT_FOUND = 12150,
                /// <summary>
                /// The supplied header is invalid.
                /// </summary>
                ERROR_HTTP_INVALID_HEADER = 12153,
                /// <summary>
                /// The request made to \nHttpQueryInfo is invalid.
                /// </summary>
                ERROR_HTTP_INVALID_QUERY_REQUEST = 12154,
                /// <summary>
                /// The server response could not be parsed.
                /// </summary>
                ERROR_HTTP_INVALID_SERVER_RESPONSE = 12152,
                /// <summary>
                /// The HTTP request was not redirected.
                /// </summary>
                ERROR_HTTP_NOT_REDIRECTED = 12160,
                /// <summary>
                /// The redirection failed because either the scheme changed (for example, HTTP to FTP) or all attempts made to redirect failed (default is five attempts).
                /// </summary>
                ERROR_HTTP_REDIRECT_FAILED = 12156,
                /// <summary>
                /// The redirection requires user confirmation.
                /// </summary>
                ERROR_HTTP_REDIRECT_NEEDS_CONFIRMATION = 12168,
                /// <summary>
                /// The application could not start an asynchronous thread.
                /// </summary>
                ERROR_INTERNET_ASYNC_THREAD_FAILED = 12047,
                /// <summary>
                /// There was an error in the automatic proxy configuration script.
                /// </summary>
                ERROR_INTERNET_BAD_AUTO_PROXY_SCRIPT = 12166,
                /// <summary>
                /// The length of an option supplied to \nInternetQueryOption or \nInternetSetOption is incorrect for the type of option specified.
                /// </summary>
                ERROR_INTERNET_BAD_OPTION_LENGTH = 12010,
                /// <summary>
                /// A required registry value was located but is an incorrect type or has an invalid value.
                /// </summary>
                ERROR_INTERNET_BAD_REGISTRY_PARAMETER = 12022,
                /// <summary>
                /// The attempt to connect to the server failed.
                /// </summary>
                ERROR_INTERNET_CANNOT_CONNECT = 12029,
                /// <summary>
                /// The application is posting and attempting to change multiple lines of text on a server that is not secure.
                /// </summary>
                ERROR_INTERNET_CHG_POST_IS_NON_SECURE = 12042,
                /// <summary>
                /// The server is requesting client authentication.
                /// </summary>
                ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED = 12044,
                /// <summary>
                /// Client authorization is not set up on this computer.
                /// </summary>
                ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP = 12046,
                /// <summary>
                /// The connection with the server has been terminated.
                /// </summary>
                ERROR_INTERNET_CONNECTION_ABORTED = 12030,
                /// <summary>
                /// The connection with the server has been reset.
                /// </summary>
                ERROR_INTERNET_CONNECTION_RESET = 12031,
                /// <summary>
                /// WinINet failed to perform content decoding on the response. For more information, see the Content Encoding topic.
                /// </summary>
                ERROR_INTERNET_DECODING_FAILED = 12175,
                /// <summary>
                /// Another thread has a password dialog box in progress.
                /// </summary>
                ERROR_INTERNET_DIALOG_PENDING = 12049,
                /// <summary>
                /// The Internet connection has been lost.
                /// </summary>
                ERROR_INTERNET_DISCONNECTED = 12163,
                /// <summary>
                /// An extended error was returned from the server. This is typically a string or buffer containing a verbose error message. Call \nInternetGetLastResponseInfo to retrieve the error text.
                /// </summary>
                ERROR_INTERNET_EXTENDED_ERROR = 12003,
                /// <summary>
                /// The function failed due to a security check.
                /// </summary>
                ERROR_INTERNET_FAILED_DUETOSECURITYCHECK = 12171,
                /// <summary>
                /// The function needs to redo the request.
                /// </summary>
                ERROR_INTERNET_FORCE_RETRY = 12032,
                /// <summary>
                /// The requested resource requires Fortezza authentication.
                /// </summary>
                ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED = 12054,
                /// <summary>
                /// The request failed because the handle already exists.
                /// </summary>
                ERROR_INTERNET_HANDLE_EXISTS = 12036,
                /// <summary>
                /// The application is moving from a non-SSL to an SSL connection because of a redirect.
                /// </summary>
                ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR = 12039,
                /// <summary>
                /// The data being submitted to an SSL connection is being redirected to a non-SSL connection.
                /// </summary>
                ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR = 12052,
                /// <summary>
                /// The application is moving from an SSL to an non-SSL connection because of a redirect.
                /// </summary>
                ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR = 12040,
                /// <summary>
                /// The format of the request is invalid.
                /// </summary>
                ERROR_INTERNET_INCORRECT_FORMAT = 12027,
                /// <summary>
                /// The requested operation cannot be carried out because the handle supplied is not in the correct state.
                /// </summary>
                ERROR_INTERNET_INCORRECT_HANDLE_STATE = 12019,
                /// <summary>
                /// The type of handle supplied is incorrect for this operation.
                /// </summary>
                ERROR_INTERNET_INCORRECT_HANDLE_TYPE = 12018,
                /// <summary>
                /// The request to connect and log on to an FTP server could not be completed because the supplied password is incorrect.
                /// </summary>
                ERROR_INTERNET_INCORRECT_PASSWORD = 12014,
                /// <summary>
                /// The request to connect and log on to an FTP server could not be completed because the supplied user name is incorrect.
                /// </summary>
                ERROR_INTERNET_INCORRECT_USER_NAME = 12013,
                /// <summary>
                /// The request requires a CD-ROM to be inserted in the CD-ROM drive to locate the resource requested.Note  Windows Vista and Windows Server 2008 and earlier only.
                /// </summary>
                ERROR_INTERNET_INSERT_CDROM = 12053,
                /// <summary>
                /// An internal error has occurred.
                /// </summary>
                ERROR_INTERNET_INTERNAL_ERROR = 12004,
                /// <summary>
                /// The function is unfamiliar with the Certificate Authority that generated the server's certificate.
                /// </summary>
                ERROR_INTERNET_INVALID_CA = 12045,
                /// <summary>
                /// The requested operation is invalid.
                /// </summary>
                ERROR_INTERNET_INVALID_OPERATION = 12016,
                /// <summary>
                /// A request to \nInternetQueryOption or \nInternetSetOption specified an invalid option value.
                /// </summary>
                ERROR_INTERNET_INVALID_OPTION = 12009,
                /// <summary>
                /// The request to the proxy was invalid.
                /// </summary>
                ERROR_INTERNET_INVALID_PROXY_REQUEST = 12033,
                /// <summary>
                /// The URL is invalid.
                /// </summary>
                ERROR_INTERNET_INVALID_URL = 12005,
                /// <summary>
                /// The requested item could not be located.
                /// </summary>
                ERROR_INTERNET_ITEM_NOT_FOUND = 12028,
                /// <summary>
                /// The request to connect and log on to an FTP server failed.
                /// </summary>
                ERROR_INTERNET_LOGIN_FAILURE = 12015,
                /// <summary>
                /// The MS-Logoff digest header has been returned from the website. This header specifically instructs the digest package to purge credentials for the associated realm. This error will only be returned if \nINTERNET_ERROR_MASK_LOGIN_FAILURE_DISPLAY_ENTITY_BODY option has been set; otherwise, ERROR_INTERNET_LOGIN_FAILURE is returned.
                /// </summary>
                ERROR_INTERNET_LOGIN_FAILURE_DISPLAY_ENTITY_BODY = 12174,
                /// <summary>
                /// The content is not entirely secure. Some of the content being viewed may have come from unsecured servers.
                /// </summary>
                ERROR_INTERNET_MIXED_SECURITY = 12041,
                /// <summary>
                /// The server name could not be resolved.
                /// </summary>
                ERROR_INTERNET_NAME_NOT_RESOLVED = 12007,
                /// <summary>
                /// Not currently implemented.
                /// </summary>
                ERROR_INTERNET_NEED_MSN_SSPI_PKG = 12173,
                /// <summary>
                /// A user interface or other blocking operation has been requested.Note  Windows Vista and Windows Server 2008 and earlier only.
                /// </summary>
                ERROR_INTERNET_NEED_UI = 12034,
                /// <summary>
                /// An asynchronous request could not be made because a callback function has not been set.
                /// </summary>
                ERROR_INTERNET_NO_CALLBACK = 12025,
                /// <summary>
                /// An asynchronous request could not be made because a zero context value was supplied.
                /// </summary>
                ERROR_INTERNET_NO_CONTEXT = 12024,
                /// <summary>
                /// Direct network access cannot be made at this time.
                /// </summary>
                ERROR_INTERNET_NO_DIRECT_ACCESS = 12023,
                /// <summary>
                /// Initialization of the WinINet API has not occurred. Indicates that a higher-level function, such as \nInternetOpen, has not been called yet.
                /// </summary>
                ERROR_INTERNET_NOT_INITIALIZED = 12172,
                /// <summary>
                /// The request cannot be made via a proxy.
                /// </summary>
                ERROR_INTERNET_NOT_PROXY_REQUEST = 12020,
                /// <summary>
                /// The operation was canceled, usually because the handle on which the request was operating was closed before the operation completed.
                /// </summary>
                ERROR_INTERNET_OPERATION_CANCELLED = 12017,
                /// <summary>
                /// The requested option cannot be set, only queried.
                /// </summary>
                ERROR_INTERNET_OPTION_NOT_SETTABLE = 12011,
                /// <summary>
                /// No more handles could be generated at this time.
                /// </summary>
                ERROR_INTERNET_OUT_OF_HANDLES = 12001,
                /// <summary>
                /// The application is posting data to a server that is not secure.
                /// </summary>
                ERROR_INTERNET_POST_IS_NON_SECURE = 12043,
                /// <summary>
                /// The requested protocol could not be located.
                /// </summary>
                ERROR_INTERNET_PROTOCOL_NOT_FOUND = 12008,
                /// <summary>
                /// The designated proxy server cannot be reached.
                /// </summary>
                ERROR_INTERNET_PROXY_SERVER_UNREACHABLE = 12165,
                /// <summary>
                /// The function could not handle the redirection, because the scheme changed (for example, HTTP to FTP).
                /// </summary>
                ERROR_INTERNET_REDIRECT_SCHEME_CHANGE = 12048,
                /// <summary>
                /// A required registry value could not be located.
                /// </summary>
                ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND = 12021,
                /// <summary>
                /// The required operation could not be completed because one or more requests are pending.
                /// </summary>
                ERROR_INTERNET_REQUEST_PENDING = 12026,
                /// <summary>
                /// The dialog box should be retried.
                /// </summary>
                ERROR_INTERNET_RETRY_DIALOG = 12050,
                /// <summary>
                /// SSL certificate common name (host name field) is incorrect—for example, if you entered www.server.com and the common name on the certificate says www.different.com.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_CN_INVALID = 12038,
                /// <summary>
                /// SSL certificate date that was received from the server is bad. The certificate is expired.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_DATE_INVALID = 12037,
                /// <summary>
                /// The SSL certificate contains errors.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_ERRORS = 12055,
                /// <summary>
                /// The SSL certificate was not revoked.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_NO_REV = 12056,
                /// <summary>
                /// Revocation of the SSL certificate failed.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_REV_FAILED = 12057,
                /// <summary>
                /// The SSL certificate was revoked.
                /// </summary>
                ERROR_INTERNET_SEC_CERT_REVOKED = 12170,
                /// <summary>
                /// The SSL certificate is invalid.
                /// </summary>
                ERROR_INTERNET_SEC_INVALID_CERT = 12169,
                /// <summary>
                /// The application experienced an internal error loading the SSL libraries.
                /// </summary>
                ERROR_INTERNET_SECURITY_CHANNEL_ERROR = 12157,
                /// <summary>
                /// The website or server indicated is unreachable.
                /// </summary>
                ERROR_INTERNET_SERVER_UNREACHABLE = 12164,
                /// <summary>
                /// WinINet support is being shut down or unloaded.
                /// </summary>
                ERROR_INTERNET_SHUTDOWN = 12012,
                /// <summary>
                /// The required protocol stack is not loaded and the application cannot start WinSock.
                /// </summary>
                ERROR_INTERNET_TCPIP_NOT_INSTALLED = 12159,
                /// <summary>
                /// The request has timed out.
                /// </summary>
                ERROR_INTERNET_TIMEOUT = 12002,
                /// <summary>
                /// The function was unable to cache the file.
                /// </summary>
                ERROR_INTERNET_UNABLE_TO_CACHE_FILE = 12158,
                /// <summary>
                /// The automatic proxy configuration script could not be downloaded. The INTERNET_FLAG_MUST_CACHE_REQUEST flag was set.
                /// </summary>
                ERROR_INTERNET_UNABLE_TO_DOWNLOAD_SCRIPT = 12167,
                /// <summary>
                /// The URL scheme could not be recognized, or is not supported.
                /// </summary>
                ERROR_INTERNET_UNRECOGNIZED_SCHEME = 12006,
                /// <summary>
                /// The specified quick mode policy already exists.
                /// </summary>
                ERROR_IPSEC_QM_POLICY_EXISTS = 13000,
                /// <summary>
                /// The specified quick mode policy was not found.
                /// </summary>
                ERROR_IPSEC_QM_POLICY_NOT_FOUND = 13001,
                /// <summary>
                /// The specified quick mode policy is being used.
                /// </summary>
                ERROR_IPSEC_QM_POLICY_IN_USE = 13002,
                /// <summary>
                /// The specified main mode policy already exists.
                /// </summary>
                ERROR_IPSEC_MM_POLICY_EXISTS = 13003,
                /// <summary>
                /// The specified main mode policy was not found.
                /// </summary>
                ERROR_IPSEC_MM_POLICY_NOT_FOUND = 13004,
                /// <summary>
                /// The specified main mode policy is being used.
                /// </summary>
                ERROR_IPSEC_MM_POLICY_IN_USE = 13005,
                /// <summary>
                /// The specified main mode filter already exists.
                /// </summary>
                ERROR_IPSEC_MM_FILTER_EXISTS = 13006,
                /// <summary>
                /// The specified main mode filter was not found.
                /// </summary>
                ERROR_IPSEC_MM_FILTER_NOT_FOUND = 13007,
                /// <summary>
                /// The specified transport mode filter already exists.
                /// </summary>
                ERROR_IPSEC_TRANSPORT_FILTER_EXISTS = 13008,
                /// <summary>
                /// The specified transport mode filter does not exist.
                /// </summary>
                ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND = 13009,
                /// <summary>
                /// The specified main mode authentication list exists.
                /// </summary>
                ERROR_IPSEC_MM_AUTH_EXISTS = 13010,
                /// <summary>
                /// The specified main mode authentication list was not found.
                /// </summary>
                ERROR_IPSEC_MM_AUTH_NOT_FOUND = 13011,
                /// <summary>
                /// The specified main mode authentication list is being used.
                /// </summary>
                ERROR_IPSEC_MM_AUTH_IN_USE = 13012,
                /// <summary>
                /// The specified default main mode policy was not found.
                /// </summary>
                ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND = 13013,
                /// <summary>
                /// The specified default main mode authentication list was not found.
                /// </summary>
                ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND = 13014,
                /// <summary>
                /// The specified default quick mode policy was not found.
                /// </summary>
                ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND = 13015,
                /// <summary>
                /// The specified tunnel mode filter exists.
                /// </summary>
                ERROR_IPSEC_TUNNEL_FILTER_EXISTS = 13016,
                /// <summary>
                /// The specified tunnel mode filter was not found.
                /// </summary>
                ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND = 13017,
                /// <summary>
                /// The Main Mode filter is pending deletion.
                /// </summary>
                ERROR_IPSEC_MM_FILTER_PENDING_DELETION = 13018,
                /// <summary>
                /// The transport filter is pending deletion.
                /// </summary>
                ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION = 13019,
                /// <summary>
                /// The tunnel filter is pending deletion.
                /// </summary>
                ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION = 13020,
                /// <summary>
                /// The Main Mode policy is pending deletion.
                /// </summary>
                ERROR_IPSEC_MM_POLICY_PENDING_DELETION = 13021,
                /// <summary>
                /// The Main Mode authentication bundle is pending deletion.
                /// </summary>
                ERROR_IPSEC_MM_AUTH_PENDING_DELETION = 13022,
                /// <summary>
                /// The Quick Mode policy is pending deletion.
                /// </summary>
                ERROR_IPSEC_QM_POLICY_PENDING_DELETION = 13023,
                /// <summary>
                /// The Main Mode policy was successfully added, but some of the requested offers are not supported.
                /// </summary>
                WARNING_IPSEC_MM_POLICY_PRUNED = 13024,
                /// <summary>
                /// The Quick Mode policy was successfully added, but some of the requested offers are not supported.
                /// </summary>
                WARNING_IPSEC_QM_POLICY_PRUNED = 13025,
                /// <summary>
                /// ERROR_IPSEC_IKE_NEG_STATUS_BEGIN
                /// </summary>
                ERROR_IPSEC_IKE_NEG_STATUS_BEGIN = 13800,
                /// <summary>
                /// IKE authentication credentials are unacceptable.
                /// </summary>
                ERROR_IPSEC_IKE_AUTH_FAIL = 13801,
                /// <summary>
                /// IKE security attributes are unacceptable.
                /// </summary>
                ERROR_IPSEC_IKE_ATTRIB_FAIL = 13802,
                /// <summary>
                /// IKE Negotiation in progress.
                /// </summary>
                ERROR_IPSEC_IKE_NEGOTIATION_PENDING = 13803,
                /// <summary>
                /// General processing error.
                /// </summary>
                ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR = 13804,
                /// <summary>
                /// Negotiation timed out.
                /// </summary>
                ERROR_IPSEC_IKE_TIMED_OUT = 13805,
                /// <summary>
                /// IKE failed to find valid machine certificate. Contact your Network Security Administrator about installing a valid certificate in the appropriate Certificate Store.
                /// </summary>
                ERROR_IPSEC_IKE_NO_CERT = 13806,
                /// <summary>
                /// IKE SA deleted by peer before establishment completed.
                /// </summary>
                ERROR_IPSEC_IKE_SA_DELETED = 13807,
                /// <summary>
                /// IKE SA deleted before establishment completed.
                /// </summary>
                ERROR_IPSEC_IKE_SA_REAPED = 13808,
                /// <summary>
                /// Negotiation request sat in Queue too long.
                /// </summary>
                ERROR_IPSEC_IKE_MM_ACQUIRE_DROP = 13809,
                /// <summary>
                /// Negotiation request sat in Queue too long.
                /// </summary>
                ERROR_IPSEC_IKE_QM_ACQUIRE_DROP = 13810,
                /// <summary>
                /// Negotiation request sat in Queue too long.
                /// </summary>
                ERROR_IPSEC_IKE_QUEUE_DROP_MM = 13811,
                /// <summary>
                /// Negotiation request sat in Queue too long.
                /// </summary>
                ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM = 13812,
                /// <summary>
                /// No response from peer.
                /// </summary>
                ERROR_IPSEC_IKE_DROP_NO_RESPONSE = 13813,
                /// <summary>
                /// Negotiation took too long.
                /// </summary>
                ERROR_IPSEC_IKE_MM_DELAY_DROP = 13814,
                /// <summary>
                /// Negotiation took too long.
                /// </summary>
                ERROR_IPSEC_IKE_QM_DELAY_DROP = 13815,
                /// <summary>
                /// Unknown error occurred.
                /// </summary>
                ERROR_IPSEC_IKE_ERROR = 13816,
                /// <summary>
                /// Certificate Revocation Check failed.
                /// </summary>
                ERROR_IPSEC_IKE_CRL_FAILED = 13817,
                /// <summary>
                /// Invalid certificate key usage.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_KEY_USAGE = 13818,
                /// <summary>
                /// Invalid certificate type.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_CERT_TYPE = 13819,
                /// <summary>
                /// IKE negotiation failed because the machine certificate used does not have a private key. IPsec certificates require a private key. Contact your Network Security administrator about replacing with a certificate that has a private key.
                /// </summary>
                ERROR_IPSEC_IKE_NO_PRIVATE_KEY = 13820,
                /// <summary>
                /// Simultaneous rekeys were detected.
                /// </summary>
                ERROR_IPSEC_IKE_SIMULTANEOUS_REKEY = 13821,
                /// <summary>
                /// Failure in Diffie-Hellman computation.
                /// </summary>
                ERROR_IPSEC_IKE_DH_FAIL = 13822,
                /// <summary>
                /// Don't know how to process critical payload.
                /// </summary>
                ERROR_IPSEC_IKE_CRITICAL_PAYLOAD_NOT_RECOGNIZED = 13823,
                /// <summary>
                /// Invalid header.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_HEADER = 13824,
                /// <summary>
                /// No policy configured.
                /// </summary>
                ERROR_IPSEC_IKE_NO_POLICY = 13825,
                /// <summary>
                /// Failed to verify signature.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_SIGNATURE = 13826,
                /// <summary>
                /// Failed to authenticate using Kerberos.
                /// </summary>
                ERROR_IPSEC_IKE_KERBEROS_ERROR = 13827,
                /// <summary>
                /// Peer's certificate did not have a public key.
                /// </summary>
                ERROR_IPSEC_IKE_NO_PUBLIC_KEY = 13828,
                /// <summary>
                /// Error processing error payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR = 13829,
                /// <summary>
                /// Error processing SA payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_SA = 13830,
                /// <summary>
                /// Error processing Proposal payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_PROP = 13831,
                /// <summary>
                /// Error processing Transform payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_TRANS = 13832,
                /// <summary>
                /// Error processing KE payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_KE = 13833,
                /// <summary>
                /// Error processing ID payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_ID = 13834,
                /// <summary>
                /// Error processing Cert payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_CERT = 13835,
                /// <summary>
                /// Error processing Certificate Request payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ = 13836,
                /// <summary>
                /// Error processing Hash payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_HASH = 13837,
                /// <summary>
                /// Error processing Signature payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_SIG = 13838,
                /// <summary>
                /// Error processing Nonce payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_NONCE = 13839,
                /// <summary>
                /// Error processing Notify payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY = 13840,
                /// <summary>
                /// Error processing Delete Payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_DELETE = 13841,
                /// <summary>
                /// Error processing VendorId payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR = 13842,
                /// <summary>
                /// Invalid payload received.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_PAYLOAD = 13843,
                /// <summary>
                /// Soft SA loaded.
                /// </summary>
                ERROR_IPSEC_IKE_LOAD_SOFT_SA = 13844,
                /// <summary>
                /// Soft SA torn down.
                /// </summary>
                ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN = 13845,
                /// <summary>
                /// Invalid cookie received.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_COOKIE = 13846,
                /// <summary>
                /// Peer failed to send valid machine certificate.
                /// </summary>
                ERROR_IPSEC_IKE_NO_PEER_CERT = 13847,
                /// <summary>
                /// Certification Revocation check of peer's certificate failed.
                /// </summary>
                ERROR_IPSEC_IKE_PEER_CRL_FAILED = 13848,
                /// <summary>
                /// New policy invalidated SAs formed with old policy.
                /// </summary>
                ERROR_IPSEC_IKE_POLICY_CHANGE = 13849,
                /// <summary>
                /// There is no available Main Mode IKE policy.
                /// </summary>
                ERROR_IPSEC_IKE_NO_MM_POLICY = 13850,
                /// <summary>
                /// Failed to enabled TCB privilege.
                /// </summary>
                ERROR_IPSEC_IKE_NOTCBPRIV = 13851,
                /// <summary>
                /// Failed to load SECURITY.DLL.
                /// </summary>
                ERROR_IPSEC_IKE_SECLOADFAIL = 13852,
                /// <summary>
                /// Failed to obtain security function table dispatch address from SSPI.
                /// </summary>
                ERROR_IPSEC_IKE_FAILSSPINIT = 13853,
                /// <summary>
                /// Failed to query Kerberos package to obtain max token size.
                /// </summary>
                ERROR_IPSEC_IKE_FAILQUERYSSP = 13854,
                /// <summary>
                /// Failed to obtain Kerberos server credentials for ISAKMP/ERROR_IPSEC_IKE service. Kerberos authentication will not function. The most likely reason for this is lack of domain membership. This is normal if your computer is a member of a workgroup.
                /// </summary>
                ERROR_IPSEC_IKE_SRVACQFAIL = 13855,
                /// <summary>
                /// Failed to determine SSPI principal name for ISAKMP/ERROR_IPSEC_IKE service (QueryCredentialsAttributes).
                /// </summary>
                ERROR_IPSEC_IKE_SRVQUERYCRED = 13856,
                /// <summary>
                /// Failed to obtain new SPI for the inbound SA from IPsec driver. The most common cause for this is that the driver does not have the correct filter. Check your policy to verify the filters.
                /// </summary>
                ERROR_IPSEC_IKE_GETSPIFAIL = 13857,
                /// <summary>
                /// Given filter is invalid.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_FILTER = 13858,
                /// <summary>
                /// Memory allocation failed.
                /// </summary>
                ERROR_IPSEC_IKE_OUT_OF_MEMORY = 13859,
                /// <summary>
                /// Failed to add Security Association to IPsec Driver. The most common cause for this is if the IKE negotiation took too long to complete. If the problem persists, reduce the load on the faulting machine.
                /// </summary>
                ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED = 13860,
                /// <summary>
                /// Invalid policy.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_POLICY = 13861,
                /// <summary>
                /// Invalid DOI.
                /// </summary>
                ERROR_IPSEC_IKE_UNKNOWN_DOI = 13862,
                /// <summary>
                /// Invalid situation.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_SITUATION = 13863,
                /// <summary>
                /// Diffie-Hellman failure.
                /// </summary>
                ERROR_IPSEC_IKE_DH_FAILURE = 13864,
                /// <summary>
                /// Invalid Diffie-Hellman group.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_GROUP = 13865,
                /// <summary>
                /// Error encrypting payload.
                /// </summary>
                ERROR_IPSEC_IKE_ENCRYPT = 13866,
                /// <summary>
                /// Error decrypting payload.
                /// </summary>
                ERROR_IPSEC_IKE_DECRYPT = 13867,
                /// <summary>
                /// Policy match error.
                /// </summary>
                ERROR_IPSEC_IKE_POLICY_MATCH = 13868,
                /// <summary>
                /// Unsupported ID.
                /// </summary>
                ERROR_IPSEC_IKE_UNSUPPORTED_ID = 13869,
                /// <summary>
                /// Hash verification failed.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_HASH = 13870,
                /// <summary>
                /// Invalid hash algorithm.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_HASH_ALG = 13871,
                /// <summary>
                /// Invalid hash size.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_HASH_SIZE = 13872,
                /// <summary>
                /// Invalid encryption algorithm.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG = 13873,
                /// <summary>
                /// Invalid authentication algorithm.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_AUTH_ALG = 13874,
                /// <summary>
                /// Invalid certificate signature.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_SIG = 13875,
                /// <summary>
                /// Load failed.
                /// </summary>
                ERROR_IPSEC_IKE_LOAD_FAILED = 13876,
                /// <summary>
                /// Deleted via RPC call.
                /// </summary>
                ERROR_IPSEC_IKE_RPC_DELETE = 13877,
                /// <summary>
                /// Temporary state created to perform reinitialization. This is not a real failure.
                /// </summary>
                ERROR_IPSEC_IKE_BENIGN_REINIT = 13878,
                /// <summary>
                /// The lifetime value received in the Responder Lifetime Notify is below the Windows 2000 configured minimum value. Please fix the policy on the peer machine.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY = 13879,
                /// <summary>
                /// The recipient cannot handle version of IKE specified in the header.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_MAJOR_VERSION = 13880,
                /// <summary>
                /// Key length in certificate is too small for configured security requirements.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN = 13881,
                /// <summary>
                /// Max number of established MM SAs to peer exceeded.
                /// </summary>
                ERROR_IPSEC_IKE_MM_LIMIT = 13882,
                /// <summary>
                /// IKE received a policy that disables negotiation.
                /// </summary>
                ERROR_IPSEC_IKE_NEGOTIATION_DISABLED = 13883,
                /// <summary>
                /// Reached maximum quick mode limit for the main mode. New main mode will be started.
                /// </summary>
                ERROR_IPSEC_IKE_QM_LIMIT = 13884,
                /// <summary>
                /// Main mode SA lifetime expired or peer sent a main mode delete.
                /// </summary>
                ERROR_IPSEC_IKE_MM_EXPIRED = 13885,
                /// <summary>
                /// Main mode SA assumed to be invalid because peer stopped responding.
                /// </summary>
                ERROR_IPSEC_IKE_PEER_MM_ASSUMED_INVALID = 13886,
                /// <summary>
                /// Certificate doesn't chain to a trusted root in IPsec policy.
                /// </summary>
                ERROR_IPSEC_IKE_CERT_CHAIN_POLICY_MISMATCH = 13887,
                /// <summary>
                /// Received unexpected message ID.
                /// </summary>
                ERROR_IPSEC_IKE_UNEXPECTED_MESSAGE_ID = 13888,
                /// <summary>
                /// Received invalid authentication offers.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_AUTH_PAYLOAD = 13889,
                /// <summary>
                /// Sent DoS cookie notify to initiator.
                /// </summary>
                ERROR_IPSEC_IKE_DOS_COOKIE_SENT = 13890,
                /// <summary>
                /// IKE service is shutting down.
                /// </summary>
                ERROR_IPSEC_IKE_SHUTTING_DOWN = 13891,
                /// <summary>
                /// Could not verify binding between CGA address and certificate.
                /// </summary>
                ERROR_IPSEC_IKE_CGA_AUTH_FAILED = 13892,
                /// <summary>
                /// Error processing NatOA payload.
                /// </summary>
                ERROR_IPSEC_IKE_PROCESS_ERR_NATOA = 13893,
                /// <summary>
                /// Parameters of the main mode are invalid for this quick mode.
                /// </summary>
                ERROR_IPSEC_IKE_INVALID_MM_FOR_QM = 13894,
                /// <summary>
                /// Quick mode SA was expired by IPsec driver.
                /// </summary>
                ERROR_IPSEC_IKE_QM_EXPIRED = 13895,
                /// <summary>
                /// Too many dynamically added IKEEXT filters were detected.
                /// </summary>
                ERROR_IPSEC_IKE_TOO_MANY_FILTERS = 13896,
                /// <summary>
                /// ERROR_IPSEC_IKE_NEG_STATUS_END
                /// </summary>
                ERROR_IPSEC_IKE_NEG_STATUS_END = 13897,
                /// <summary>
                /// NAP reauth succeeded and must delete the dummy NAP IKEv2 tunnel.
                /// </summary>
                ERROR_IPSEC_IKE_KILL_DUMMY_NAP_TUNNEL = 13898,
                /// <summary>
                /// Error in assigning inner IP address to initiator in tunnel mode.
                /// </summary>
                ERROR_IPSEC_IKE_INNER_IP_ASSIGNMENT_FAILURE = 13899,
                /// <summary>
                /// Require configuration payload missing.
                /// </summary>
                ERROR_IPSEC_IKE_REQUIRE_CP_PAYLOAD_MISSING = 13900,
                /// <summary>
                /// A negotiation running as the security principle who issued the connection is in progress.
                /// </summary>
                ERROR_IPSEC_KEY_MODULE_IMPERSONATION_NEGOTIATION_PENDING = 13901,
                /// <summary>
                /// SA was deleted due to IKEv1/AuthIP co-existence suppress check.
                /// </summary>
                ERROR_IPSEC_IKE_COEXISTENCE_SUPPRESS = 13902,
                /// <summary>
                /// Incoming SA request was dropped due to peer IP address rate limiting.
                /// </summary>
                ERROR_IPSEC_IKE_RATELIMIT_DROP = 13903,
                /// <summary>
                /// Peer does not support MOBIKE.
                /// </summary>
                ERROR_IPSEC_IKE_PEER_DOESNT_SUPPORT_MOBIKE = 13904,
                /// <summary>
                /// SA establishment is not authorized.
                /// </summary>
                ERROR_IPSEC_IKE_AUTHORIZATION_FAILURE = 13905,
                /// <summary>
                /// SA establishment is not authorized because there is not a sufficiently strong PKINIT-based credential.
                /// </summary>
                ERROR_IPSEC_IKE_STRONG_CRED_AUTHORIZATION_FAILURE = 13906,
                /// <summary>
                /// SA establishment is not authorized.  You may need to enter updated or different credentials such as a smartcard.
                /// </summary>
                ERROR_IPSEC_IKE_AUTHORIZATION_FAILURE_WITH_OPTIONAL_RETRY = 13907,
                /// <summary>
                /// SA establishment is not authorized because there is not a sufficiently strong PKINIT-based credential. This might be related to certificate-to-account mapping failure for the SA.
                /// </summary>
                ERROR_IPSEC_IKE_STRONG_CRED_AUTHORIZATION_AND_CERTMAP_FAILURE = 13908,
                /// <summary>
                /// ERROR_IPSEC_IKE_NEG_STATUS_EXTENDED_END
                /// </summary>
                ERROR_IPSEC_IKE_NEG_STATUS_EXTENDED_END = 13909,
                /// <summary>
                /// The SPI in the packet does not match a valid IPsec SA.
                /// </summary>
                ERROR_IPSEC_BAD_SPI = 13910,
                /// <summary>
                /// Packet was received on an IPsec SA whose lifetime has expired.
                /// </summary>
                ERROR_IPSEC_SA_LIFETIME_EXPIRED = 13911,
                /// <summary>
                /// Packet was received on an IPsec SA that does not match the packet characteristics.
                /// </summary>
                ERROR_IPSEC_WRONG_SA = 13912,
                /// <summary>
                /// Packet sequence number replay check failed.
                /// </summary>
                ERROR_IPSEC_REPLAY_CHECK_FAILED = 13913,
                /// <summary>
                /// IPsec header and/or trailer in the packet is invalid.
                /// </summary>
                ERROR_IPSEC_INVALID_PACKET = 13914,
                /// <summary>
                /// IPsec integrity check failed.
                /// </summary>
                ERROR_IPSEC_INTEGRITY_CHECK_FAILED = 13915,
                /// <summary>
                /// IPsec dropped a clear text packet.
                /// </summary>
                ERROR_IPSEC_CLEAR_TEXT_DROP = 13916,
                /// <summary>
                /// IPsec dropped an incoming ESP packet in authenticated firewall mode. This drop is benign.
                /// </summary>
                ERROR_IPSEC_AUTH_FIREWALL_DROP = 13917,
                /// <summary>
                /// IPsec dropped a packet due to DoS throttling.
                /// </summary>
                ERROR_IPSEC_THROTTLE_DROP = 13918,
                /// <summary>
                /// IPsec DoS Protection matched an explicit block rule.
                /// </summary>
                ERROR_IPSEC_DOSP_BLOCK = 13925,
                /// <summary>
                /// IPsec DoS Protection received an IPsec specific multicast packet which is not allowed.
                /// </summary>
                ERROR_IPSEC_DOSP_RECEIVED_MULTICAST = 13926,
                /// <summary>
                /// IPsec DoS Protection received an incorrectly formatted packet.
                /// </summary>
                ERROR_IPSEC_DOSP_INVALID_PACKET = 13927,
                /// <summary>
                /// IPsec DoS Protection failed to look up state.
                /// </summary>
                ERROR_IPSEC_DOSP_STATE_LOOKUP_FAILED = 13928,
                /// <summary>
                /// IPsec DoS Protection failed to create state because the maximum number of entries allowed by policy has been reached.
                /// </summary>
                ERROR_IPSEC_DOSP_MAX_ENTRIES = 13929,
                /// <summary>
                /// IPsec DoS Protection received an IPsec negotiation packet for a keying module which is not allowed by policy.
                /// </summary>
                ERROR_IPSEC_DOSP_KEYMOD_NOT_ALLOWED = 13930,
                /// <summary>
                /// IPsec DoS Protection has not been enabled.
                /// </summary>
                ERROR_IPSEC_DOSP_NOT_INSTALLED = 13931,
                /// <summary>
                /// IPsec DoS Protection failed to create a per internal IP rate limit queue because the maximum number of queues allowed by policy has been reached.
                /// </summary>
                ERROR_IPSEC_DOSP_MAX_PER_IP_RATELIMIT_QUEUES = 13932,
                /// <summary>
                /// The requested section was not present in the activation context.
                /// </summary>
                ERROR_SXS_SECTION_NOT_FOUND = 14000,
                /// <summary>
                /// The application has failed to start because its side-by-side configuration is incorrect. Please see the application event log or use the command-line sxstrace.exe tool for more detail.
                /// </summary>
                ERROR_SXS_CANT_GEN_ACTCTX = 14001,
                /// <summary>
                /// The application binding data format is invalid.
                /// </summary>
                ERROR_SXS_INVALID_ACTCTXDATA_FORMAT = 14002,
                /// <summary>
                /// The referenced assembly is not installed on your system.
                /// </summary>
                ERROR_SXS_ASSEMBLY_NOT_FOUND = 14003,
                /// <summary>
                /// The manifest file does not begin with the required tag and format information.
                /// </summary>
                ERROR_SXS_MANIFEST_FORMAT_ERROR = 14004,
                /// <summary>
                /// The manifest file contains one or more syntax errors.
                /// </summary>
                ERROR_SXS_MANIFEST_PARSE_ERROR = 14005,
                /// <summary>
                /// The application attempted to activate a disabled activation context.
                /// </summary>
                ERROR_SXS_ACTIVATION_CONTEXT_DISABLED = 14006,
                /// <summary>
                /// The requested lookup key was not found in any active activation context.
                /// </summary>
                ERROR_SXS_KEY_NOT_FOUND = 14007,
                /// <summary>
                /// A component version required by the application conflicts with another component version already active.
                /// </summary>
                ERROR_SXS_VERSION_CONFLICT = 14008,
                /// <summary>
                /// The type requested activation context section does not match the query API used.
                /// </summary>
                ERROR_SXS_WRONG_SECTION_TYPE = 14009,
                /// <summary>
                /// Lack of system resources has required isolated activation to be disabled for the current thread of execution.
                /// </summary>
                ERROR_SXS_THREAD_QUERIES_DISABLED = 14010,
                /// <summary>
                /// An attempt to set the process default activation context failed because the process default activation context was already set.
                /// </summary>
                ERROR_SXS_PROCESS_DEFAULT_ALREADY_SET = 14011,
                /// <summary>
                /// The encoding group identifier specified is not recognized.
                /// </summary>
                ERROR_SXS_UNKNOWN_ENCODING_GROUP = 14012,
                /// <summary>
                /// The encoding requested is not recognized.
                /// </summary>
                ERROR_SXS_UNKNOWN_ENCODING = 14013,
                /// <summary>
                /// The manifest contains a reference to an invalid URI.
                /// </summary>
                ERROR_SXS_INVALID_XML_NAMESPACE_URI = 14014,
                /// <summary>
                /// The application manifest contains a reference to a dependent assembly which is not installed.
                /// </summary>
                ERROR_SXS_ROOT_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14015,
                /// <summary>
                /// The manifest for an assembly used by the application has a reference to a dependent assembly which is not installed.
                /// </summary>
                ERROR_SXS_LEAF_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14016,
                /// <summary>
                /// The manifest contains an attribute for the assembly identity which is not valid.
                /// </summary>
                ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE = 14017,
                /// <summary>
                /// The manifest is missing the required default namespace specification on the assembly element.
                /// </summary>
                ERROR_SXS_MANIFEST_MISSING_REQUIRED_DEFAULT_NAMESPACE = 14018,
                /// <summary>
                /// The manifest has a default namespace specified on the assembly element but its value is not \"urn:schemas-microsoft-com:asm.v1\".
                /// </summary>
                ERROR_SXS_MANIFEST_INVALID_REQUIRED_DEFAULT_NAMESPACE = 14019,
                /// <summary>
                /// The private manifest probed has crossed a path with an unsupported reparse point.
                /// </summary>
                ERROR_SXS_PRIVATE_MANIFEST_CROSS_PATH_WITH_REPARSE_POINT = 14020,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have files by the same name.
                /// </summary>
                ERROR_SXS_DUPLICATE_DLL_NAME = 14021,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have window classes with the same name.
                /// </summary>
                ERROR_SXS_DUPLICATE_WINDOWCLASS_NAME = 14022,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have the same COM server CLSIDs.
                /// </summary>
                ERROR_SXS_DUPLICATE_CLSID = 14023,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have proxies for the same COM interface IIDs.
                /// </summary>
                ERROR_SXS_DUPLICATE_IID = 14024,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have the same COM type library TLBIDs.
                /// </summary>
                ERROR_SXS_DUPLICATE_TLBID = 14025,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest have the same COM ProgIDs.
                /// </summary>
                ERROR_SXS_DUPLICATE_PROGID = 14026,
                /// <summary>
                /// Two or more components referenced directly or indirectly by the application manifest are different versions of the same component which is not permitted.
                /// </summary>
                ERROR_SXS_DUPLICATE_ASSEMBLY_NAME = 14027,
                /// <summary>
                /// A component's file does not match the verification information present in the component manifest.
                /// </summary>
                ERROR_SXS_FILE_HASH_MISMATCH = 14028,
                /// <summary>
                /// The policy manifest contains one or more syntax errors.
                /// </summary>
                ERROR_SXS_POLICY_PARSE_ERROR = 14029,
                /// <summary>
                /// Manifest Parse Error : A string literal was expected, but no opening quote character was found.
                /// </summary>
                ERROR_SXS_XML_E_MISSINGQUOTE = 14030,
                /// <summary>
                /// Manifest Parse Error : Incorrect syntax was used in a comment.
                /// </summary>
                ERROR_SXS_XML_E_COMMENTSYNTAX = 14031,
                /// <summary>
                /// Manifest Parse Error : A name was started with an invalid character.
                /// </summary>
                ERROR_SXS_XML_E_BADSTARTNAMECHAR = 14032,
                /// <summary>
                /// Manifest Parse Error : A name contained an invalid character.
                /// </summary>
                ERROR_SXS_XML_E_BADNAMECHAR = 14033,
                /// <summary>
                /// Manifest Parse Error : A string literal contained an invalid character.
                /// </summary>
                ERROR_SXS_XML_E_BADCHARINSTRING = 14034,
                /// <summary>
                /// Manifest Parse Error : Invalid syntax for an xml declaration.
                /// </summary>
                ERROR_SXS_XML_E_XMLDECLSYNTAX = 14035,
                /// <summary>
                /// Manifest Parse Error : An Invalid character was found in text content.
                /// </summary>
                ERROR_SXS_XML_E_BADCHARDATA = 14036,
                /// <summary>
                /// Manifest Parse Error : Required white space was missing.
                /// </summary>
                ERROR_SXS_XML_E_MISSINGWHITESPACE = 14037,
                /// <summary>
                /// Manifest Parse Error : The character '>' was expected.
                /// </summary>
                ERROR_SXS_XML_E_EXPECTINGTAGEND = 14038,
                /// <summary>
                /// Manifest Parse Error : A semi colon character was expected.
                /// </summary>
                ERROR_SXS_XML_E_MISSINGSEMICOLON = 14039,
                /// <summary>
                /// Manifest Parse Error : Unbalanced parentheses.
                /// </summary>
                ERROR_SXS_XML_E_UNBALANCEDPAREN = 14040,
                /// <summary>
                /// Manifest Parse Error : Internal error.
                /// </summary>
                ERROR_SXS_XML_E_INTERNALERROR = 14041,
                /// <summary>
                /// Manifest Parse Error : Whitespace is not allowed at this location.
                /// </summary>
                ERROR_SXS_XML_E_UNEXPECTED_WHITESPACE = 14042,
                /// <summary>
                /// Manifest Parse Error : End of file reached in invalid state for current encoding.
                /// </summary>
                ERROR_SXS_XML_E_INCOMPLETE_ENCODING = 14043,
                /// <summary>
                /// Manifest Parse Error : Missing parenthesis.
                /// </summary>
                ERROR_SXS_XML_E_MISSING_PAREN = 14044,
                /// <summary>
                /// Manifest Parse Error : A single or double closing quote character (\\' or \\\") is missing.
                /// </summary>
                ERROR_SXS_XML_E_EXPECTINGCLOSEQUOTE = 14045,
                /// <summary>
                /// Manifest Parse Error : Multiple colons are not allowed in a name.
                /// </summary>
                ERROR_SXS_XML_E_MULTIPLE_COLONS = 14046,
                /// <summary>
                /// Manifest Parse Error : Invalid character for decimal digit.
                /// </summary>
                ERROR_SXS_XML_E_INVALID_DECIMAL = 14047,
                /// <summary>
                /// Manifest Parse Error : Invalid character for hexadecimal digit.
                /// </summary>
                ERROR_SXS_XML_E_INVALID_HEXIDECIMAL = 14048,
                /// <summary>
                /// Manifest Parse Error : Invalid unicode character value for this platform.
                /// </summary>
                ERROR_SXS_XML_E_INVALID_UNICODE = 14049,
                /// <summary>
                /// Manifest Parse Error : Expecting whitespace or '?'.
                /// </summary>
                ERROR_SXS_XML_E_WHITESPACEORQUESTIONMARK = 14050,
                /// <summary>
                /// Manifest Parse Error : End tag was not expected at this location.
                /// </summary>
                ERROR_SXS_XML_E_UNEXPECTEDENDTAG = 14051,
                /// <summary>
                /// Manifest Parse Error : The following tags were not closed: %1.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDTAG = 14052,
                /// <summary>
                /// Manifest Parse Error : Duplicate attribute.
                /// </summary>
                ERROR_SXS_XML_E_DUPLICATEATTRIBUTE = 14053,
                /// <summary>
                /// Manifest Parse Error : Only one top level element is allowed in an XML document.
                /// </summary>
                ERROR_SXS_XML_E_MULTIPLEROOTS = 14054,
                /// <summary>
                /// Manifest Parse Error : Invalid at the top level of the document.
                /// </summary>
                ERROR_SXS_XML_E_INVALIDATROOTLEVEL = 14055,
                /// <summary>
                /// Manifest Parse Error : Invalid xml declaration.
                /// </summary>
                ERROR_SXS_XML_E_BADXMLDECL = 14056,
                /// <summary>
                /// Manifest Parse Error : XML document must have a top level element.
                /// </summary>
                ERROR_SXS_XML_E_MISSINGROOT = 14057,
                /// <summary>
                /// Manifest Parse Error : Unexpected end of file.
                /// </summary>
                ERROR_SXS_XML_E_UNEXPECTEDEOF = 14058,
                /// <summary>
                /// Manifest Parse Error : Parameter entities cannot be used inside markup declarations in an internal subset.
                /// </summary>
                ERROR_SXS_XML_E_BADPEREFINSUBSET = 14059,
                /// <summary>
                /// Manifest Parse Error : Element was not closed.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDSTARTTAG = 14060,
                /// <summary>
                /// Manifest Parse Error : End element was missing the character '>'.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDENDTAG = 14061,
                /// <summary>
                /// Manifest Parse Error : A string literal was not closed.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDSTRING = 14062,
                /// <summary>
                /// Manifest Parse Error : A comment was not closed.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDCOMMENT = 14063,
                /// <summary>
                /// Manifest Parse Error : A declaration was not closed.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDDECL = 14064,
                /// <summary>
                /// Manifest Parse Error : A CDATA section was not closed.
                /// </summary>
                ERROR_SXS_XML_E_UNCLOSEDCDATA = 14065,
                /// <summary>
                /// Manifest Parse Error : The namespace prefix is not allowed to start with the reserved string \"xml\".
                /// </summary>
                ERROR_SXS_XML_E_RESERVEDNAMESPACE = 14066,
                /// <summary>
                /// Manifest Parse Error : System does not support the specified encoding.
                /// </summary>
                ERROR_SXS_XML_E_INVALIDENCODING = 14067,
                /// <summary>
                /// Manifest Parse Error : Switch from current encoding to specified encoding not supported.
                /// </summary>
                ERROR_SXS_XML_E_INVALIDSWITCH = 14068,
                /// <summary>
                /// Manifest Parse Error : The name 'xml' is reserved and must be lower case.
                /// </summary>
                ERROR_SXS_XML_E_BADXMLCASE = 14069,
                /// <summary>
                /// Manifest Parse Error : The standalone attribute must have the value 'yes' or 'no'.
                /// </summary>
                ERROR_SXS_XML_E_INVALID_STANDALONE = 14070,
                /// <summary>
                /// Manifest Parse Error : The standalone attribute cannot be used in external entities.
                /// </summary>
                ERROR_SXS_XML_E_UNEXPECTED_STANDALONE = 14071,
                /// <summary>
                /// Manifest Parse Error : Invalid version number.
                /// </summary>
                ERROR_SXS_XML_E_INVALID_VERSION = 14072,
                /// <summary>
                /// Manifest Parse Error : Missing equals sign between attribute and attribute value.
                /// </summary>
                ERROR_SXS_XML_E_MISSINGEQUALS = 14073,
                /// <summary>
                /// Assembly Protection Error : Unable to recover the specified assembly.
                /// </summary>
                ERROR_SXS_PROTECTION_RECOVERY_FAILED = 14074,
                /// <summary>
                /// Assembly Protection Error : The public key for an assembly was too short to be allowed.
                /// </summary>
                ERROR_SXS_PROTECTION_PUBLIC_KEY_TOO_SHORT = 14075,
                /// <summary>
                /// Assembly Protection Error : The catalog for an assembly is not valid, or does not match the assembly's manifest.
                /// </summary>
                ERROR_SXS_PROTECTION_CATALOG_NOT_VALID = 14076,
                /// <summary>
                /// An HRESULT could not be translated to a corresponding Win32 error code.
                /// </summary>
                ERROR_SXS_UNTRANSLATABLE_HRESULT = 14077,
                /// <summary>
                /// Assembly Protection Error : The catalog for an assembly is missing.
                /// </summary>
                ERROR_SXS_PROTECTION_CATALOG_FILE_MISSING = 14078,
                /// <summary>
                /// The supplied assembly identity is missing one or more attributes which must be present in this context.
                /// </summary>
                ERROR_SXS_MISSING_ASSEMBLY_IDENTITY_ATTRIBUTE = 14079,
                /// <summary>
                /// The supplied assembly identity has one or more attribute names that contain characters not permitted in XML names.
                /// </summary>
                ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE_NAME = 14080,
                /// <summary>
                /// The referenced assembly could not be found.
                /// </summary>
                ERROR_SXS_ASSEMBLY_MISSING = 14081,
                /// <summary>
                /// The activation context activation stack for the running thread of execution is corrupt.
                /// </summary>
                ERROR_SXS_CORRUPT_ACTIVATION_STACK = 14082,
                /// <summary>
                /// The application isolation metadata for this process or thread has become corrupt.
                /// </summary>
                ERROR_SXS_CORRUPTION = 14083,
                /// <summary>
                /// The activation context being deactivated is not the most recently activated one.
                /// </summary>
                ERROR_SXS_EARLY_DEACTIVATION = 14084,
                /// <summary>
                /// The activation context being deactivated is not active for the current thread of execution.
                /// </summary>
                ERROR_SXS_INVALID_DEACTIVATION = 14085,
                /// <summary>
                /// The activation context being deactivated has already been deactivated.
                /// </summary>
                ERROR_SXS_MULTIPLE_DEACTIVATION = 14086,
                /// <summary>
                /// A component used by the isolation facility has requested to terminate the process.
                /// </summary>
                ERROR_SXS_PROCESS_TERMINATION_REQUESTED = 14087,
                /// <summary>
                /// A kernel mode component is releasing a reference on an activation context.
                /// </summary>
                ERROR_SXS_RELEASE_ACTIVATION_CONTEXT = 14088,
                /// <summary>
                /// The activation context of system default assembly could not be generated.
                /// </summary>
                ERROR_SXS_SYSTEM_DEFAULT_ACTIVATION_CONTEXT_EMPTY = 14089,
                /// <summary>
                /// The value of an attribute in an identity is not within the legal range.
                /// </summary>
                ERROR_SXS_INVALID_IDENTITY_ATTRIBUTE_VALUE = 14090,
                /// <summary>
                /// The name of an attribute in an identity is not within the legal range.
                /// </summary>
                ERROR_SXS_INVALID_IDENTITY_ATTRIBUTE_NAME = 14091,
                /// <summary>
                /// An identity contains two definitions for the same attribute.
                /// </summary>
                ERROR_SXS_IDENTITY_DUPLICATE_ATTRIBUTE = 14092,
                /// <summary>
                /// The identity string is malformed. This may be due to a trailing comma, more than two unnamed attributes, missing attribute name or missing attribute value.
                /// </summary>
                ERROR_SXS_IDENTITY_PARSE_ERROR = 14093,
                /// <summary>
                /// A string containing localized substitutable content was malformed. Either a dollar sign ($) was followed by something other than a left parenthesis or another dollar sign or an substitution's right parenthesis was not found.
                /// </summary>
                ERROR_MALFORMED_SUBSTITUTION_STRING = 14094,
                /// <summary>
                /// The public key token does not correspond to the public key specified.
                /// </summary>
                ERROR_SXS_INCORRECT_PUBLIC_KEY_TOKEN = 14095,
                /// <summary>
                /// A substitution string had no mapping.
                /// </summary>
                ERROR_UNMAPPED_SUBSTITUTION_STRING = 14096,
                /// <summary>
                /// The component must be locked before making the request.
                /// </summary>
                ERROR_SXS_ASSEMBLY_NOT_LOCKED = 14097,
                /// <summary>
                /// The component store has been corrupted.
                /// </summary>
                ERROR_SXS_COMPONENT_STORE_CORRUPT = 14098,
                /// <summary>
                /// An advanced installer failed during setup or servicing.
                /// </summary>
                ERROR_ADVANCED_INSTALLER_FAILED = 14099,
                /// <summary>
                /// The character encoding in the XML declaration did not match the encoding used in the document.
                /// </summary>
                ERROR_XML_ENCODING_MISMATCH = 14100,
                /// <summary>
                /// The identities of the manifests are identical but their contents are different.
                /// </summary>
                ERROR_SXS_MANIFEST_IDENTITY_SAME_BUT_CONTENTS_DIFFERENT = 14101,
                /// <summary>
                /// The component identities are different.
                /// </summary>
                ERROR_SXS_IDENTITIES_DIFFERENT = 14102,
                /// <summary>
                /// The assembly is not a deployment.
                /// </summary>
                ERROR_SXS_ASSEMBLY_IS_NOT_A_DEPLOYMENT = 14103,
                /// <summary>
                /// The file is not a part of the assembly.
                /// </summary>
                ERROR_SXS_FILE_NOT_PART_OF_ASSEMBLY = 14104,
                /// <summary>
                /// The size of the manifest exceeds the maximum allowed.
                /// </summary>
                ERROR_SXS_MANIFEST_TOO_BIG = 14105,
                /// <summary>
                /// The setting is not registered.
                /// </summary>
                ERROR_SXS_SETTING_NOT_REGISTERED = 14106,
                /// <summary>
                /// One or more required members of the transaction are not present.
                /// </summary>
                ERROR_SXS_TRANSACTION_CLOSURE_INCOMPLETE = 14107,
                /// <summary>
                /// The SMI primitive installer failed during setup or servicing.
                /// </summary>
                ERROR_SMI_PRIMITIVE_INSTALLER_FAILED = 14108,
                /// <summary>
                /// A generic command executable returned a result that indicates failure.
                /// </summary>
                ERROR_GENERIC_COMMAND_FAILED = 14109,
                /// <summary>
                /// A component is missing file verification information in its manifest.
                /// </summary>
                ERROR_SXS_FILE_HASH_MISSING = 14110,
                /// <summary>
                /// The specified channel path is invalid.
                /// </summary>
                ERROR_EVT_INVALID_CHANNEL_PATH = 15000,
                /// <summary>
                /// The specified query is invalid.
                /// </summary>
                ERROR_EVT_INVALID_QUERY = 15001,
                /// <summary>
                /// The publisher metadata cannot be found in the resource.
                /// </summary>
                ERROR_EVT_PUBLISHER_METADATA_NOT_FOUND = 15002,
                /// <summary>
                /// The template for an event definition cannot be found in the resource (error = %1).
                /// </summary>
                ERROR_EVT_EVENT_TEMPLATE_NOT_FOUND = 15003,
                /// <summary>
                /// The specified publisher name is invalid.
                /// </summary>
                ERROR_EVT_INVALID_PUBLISHER_NAME = 15004,
                /// <summary>
                /// The event data raised by the publisher is not compatible with the event template definition in the publisher's manifest.
                /// </summary>
                ERROR_EVT_INVALID_EVENT_DATA = 15005,
                /// <summary>
                /// The specified channel could not be found. Check channel configuration.
                /// </summary>
                ERROR_EVT_CHANNEL_NOT_FOUND = 15007,
                /// <summary>
                /// The specified xml text was not well-formed. See Extended Error for more details.
                /// </summary>
                ERROR_EVT_MALFORMED_XML_TEXT = 15008,
                /// <summary>
                /// The caller is trying to subscribe to a direct channel which is not allowed. The events for a direct channel go directly to a logfile and cannot be subscribed to.
                /// </summary>
                ERROR_EVT_SUBSCRIPTION_TO_DIRECT_CHANNEL = 15009,
                /// <summary>
                /// Configuration error.
                /// </summary>
                ERROR_EVT_CONFIGURATION_ERROR = 15010,
                /// <summary>
                /// The query result is stale / invalid. This may be due to the log being cleared or rolling over after the query result was created. Users should handle this code by releasing the query result object and reissuing the query.
                /// </summary>
                ERROR_EVT_QUERY_RESULT_STALE = 15011,
                /// <summary>
                /// Query result is currently at an invalid position.
                /// </summary>
                ERROR_EVT_QUERY_RESULT_INVALID_POSITION = 15012,
                /// <summary>
                /// Registered MSXML doesn't support validation.
                /// </summary>
                ERROR_EVT_NON_VALIDATING_MSXML = 15013,
                /// <summary>
                /// An expression can only be followed by a change of scope operation if it itself evaluates to a node set and is not already part of some other change of scope operation.
                /// </summary>
                ERROR_EVT_FILTER_ALREADYSCOPED = 15014,
                /// <summary>
                /// Can't perform a step operation from a term that does not represent an element set.
                /// </summary>
                ERROR_EVT_FILTER_NOTELTSET = 15015,
                /// <summary>
                /// Left hand side arguments to binary operators must be either attributes, nodes or variables and right hand side arguments must be constants.
                /// </summary>
                ERROR_EVT_FILTER_INVARG = 15016,
                /// <summary>
                /// A step operation must involve either a node test or, in the case of a predicate, an algebraic expression against which to test each node in the node set identified by the preceeding node set can be evaluated.
                /// </summary>
                ERROR_EVT_FILTER_INVTEST = 15017,
                /// <summary>
                /// This data type is currently unsupported.
                /// </summary>
                ERROR_EVT_FILTER_INVTYPE = 15018,
                /// <summary>
                /// A syntax error occurred at position %1!d!.
                /// </summary>
                ERROR_EVT_FILTER_PARSEERR = 15019,
                /// <summary>
                /// This operator is unsupported by this implementation of the filter.
                /// </summary>
                ERROR_EVT_FILTER_UNSUPPORTEDOP = 15020,
                /// <summary>
                /// The token encountered was unexpected.
                /// </summary>
                ERROR_EVT_FILTER_UNEXPECTEDTOKEN = 15021,
                /// <summary>
                /// The requested operation cannot be performed over an enabled direct channel. The channel must first be disabled before performing the requested operation.
                /// </summary>
                ERROR_EVT_INVALID_OPERATION_OVER_ENABLED_DIRECT_CHANNEL = 15022,
                /// <summary>
                /// Channel property %1!s! contains invalid value. The value has invalid type, is outside of valid range, can't be updated or is not supported by this type of channel.
                /// </summary>
                ERROR_EVT_INVALID_CHANNEL_PROPERTY_VALUE = 15023,
                /// <summary>
                /// Publisher property %1!s! contains invalid value. The value has invalid type, is outside of valid range, can't be updated or is not supported by this type of publisher.
                /// </summary>
                ERROR_EVT_INVALID_PUBLISHER_PROPERTY_VALUE = 15024,
                /// <summary>
                /// The channel fails to activate.
                /// </summary>
                ERROR_EVT_CHANNEL_CANNOT_ACTIVATE = 15025,
                /// <summary>
                /// The xpath expression exceeded supported complexity. Please symplify it or split it into two or more simple expressions.
                /// </summary>
                ERROR_EVT_FILTER_TOO_COMPLEX = 15026,
                /// <summary>
                /// the message resource is present but the message is not found in the string/message table.
                /// </summary>
                ERROR_EVT_MESSAGE_NOT_FOUND = 15027,
                /// <summary>
                /// The message id for the desired message could not be found.
                /// </summary>
                ERROR_EVT_MESSAGE_ID_NOT_FOUND = 15028,
                /// <summary>
                /// The substitution string for insert index (%1) could not be found.
                /// </summary>
                ERROR_EVT_UNRESOLVED_VALUE_INSERT = 15029,
                /// <summary>
                /// The description string for parameter reference (%1) could not be found.
                /// </summary>
                ERROR_EVT_UNRESOLVED_PARAMETER_INSERT = 15030,
                /// <summary>
                /// The maximum number of replacements has been reached.
                /// </summary>
                ERROR_EVT_MAX_INSERTS_REACHED = 15031,
                /// <summary>
                /// The event definition could not be found for event id (%1).
                /// </summary>
                ERROR_EVT_EVENT_DEFINITION_NOT_FOUND = 15032,
                /// <summary>
                /// The locale specific resource for the desired message is not present.
                /// </summary>
                ERROR_EVT_MESSAGE_LOCALE_NOT_FOUND = 15033,
                /// <summary>
                /// The resource is too old to be compatible.
                /// </summary>
                ERROR_EVT_VERSION_TOO_OLD = 15034,
                /// <summary>
                /// The resource is too new to be compatible.
                /// </summary>
                ERROR_EVT_VERSION_TOO_NEW = 15035,
                /// <summary>
                /// The channel at index %1!d! of the query can't be opened.
                /// </summary>
                ERROR_EVT_CANNOT_OPEN_CHANNEL_OF_QUERY = 15036,
                /// <summary>
                /// The publisher has been disabled and its resource is not available. This usually occurs when the publisher is in the process of being uninstalled or upgraded.
                /// </summary>
                ERROR_EVT_PUBLISHER_DISABLED = 15037,
                /// <summary>
                /// Attempted to create a numeric type that is outside of its valid range.
                /// </summary>
                ERROR_EVT_FILTER_OUT_OF_RANGE = 15038,
                /// <summary>
                /// The subscription fails to activate.
                /// </summary>
                ERROR_EC_SUBSCRIPTION_CANNOT_ACTIVATE = 15080,
                /// <summary>
                /// The log of the subscription is in disabled state, and can not be used to forward events to. The log must first be enabled before the subscription can be activated.
                /// </summary>
                ERROR_EC_LOG_DISABLED = 15081,
                /// <summary>
                /// When forwarding events from local machine to itself, the query of the subscription can't contain target log of the subscription.
                /// </summary>
                ERROR_EC_CIRCULAR_FORWARDING = 15082,
                /// <summary>
                /// The credential store that is used to save credentials is full.
                /// </summary>
                ERROR_EC_CREDSTORE_FULL = 15083,
                /// <summary>
                /// The credential used by this subscription can't be found in credential store.
                /// </summary>
                ERROR_EC_CRED_NOT_FOUND = 15084,
                /// <summary>
                /// No active channel is found for the query.
                /// </summary>
                ERROR_EC_NO_ACTIVE_CHANNEL = 15085,
                /// <summary>
                /// The resource loader failed to find MUI file.
                /// </summary>
                ERROR_MUI_FILE_NOT_FOUND = 15100,
                /// <summary>
                /// The resource loader failed to load MUI file because the file fail to pass validation.
                /// </summary>
                ERROR_MUI_INVALID_FILE = 15101,
                /// <summary>
                /// The RC Manifest is corrupted with garbage data or unsupported version or missing required item.
                /// </summary>
                ERROR_MUI_INVALID_RC_CONFIG = 15102,
                /// <summary>
                /// The RC Manifest has invalid culture name.
                /// </summary>
                ERROR_MUI_INVALID_LOCALE_NAME = 15103,
                /// <summary>
                /// The RC Manifest has invalid ultimatefallback name.
                /// </summary>
                ERROR_MUI_INVALID_ULTIMATEFALLBACK_NAME = 15104,
                /// <summary>
                /// The resource loader cache doesn't have loaded MUI entry.
                /// </summary>
                ERROR_MUI_FILE_NOT_LOADED = 15105,
                /// <summary>
                /// User stopped resource enumeration.
                /// </summary>
                ERROR_RESOURCE_ENUM_USER_STOP = 15106,
                /// <summary>
                /// UI language installation failed.
                /// </summary>
                ERROR_MUI_INTLSETTINGS_UILANG_NOT_INSTALLED = 15107,
                /// <summary>
                /// Locale installation failed.
                /// </summary>
                ERROR_MUI_INTLSETTINGS_INVALID_LOCALE_NAME = 15108,
                /// <summary>
                /// A resource does not have default or neutral value.
                /// </summary>
                ERROR_MRM_RUNTIME_NO_DEFAULT_OR_NEUTRAL_RESOURCE = 15110,
                /// <summary>
                /// Invalid PRI config file.
                /// </summary>
                ERROR_MRM_INVALID_PRICONFIG = 15111,
                /// <summary>
                /// Invalid file type.
                /// </summary>
                ERROR_MRM_INVALID_FILE_TYPE = 15112,
                /// <summary>
                /// Unknown qualifier.
                /// </summary>
                ERROR_MRM_UNKNOWN_QUALIFIER = 15113,
                /// <summary>
                /// Invalid qualifier value.
                /// </summary>
                ERROR_MRM_INVALID_QUALIFIER_VALUE = 15114,
                /// <summary>
                /// No Candidate found.
                /// </summary>
                ERROR_MRM_NO_CANDIDATE = 15115,
                /// <summary>
                /// The ResourceMap or NamedResource has an item that does not have default or neutral resource..
                /// </summary>
                ERROR_MRM_NO_MATCH_OR_DEFAULT_CANDIDATE = 15116,
                /// <summary>
                /// Invalid ResourceCandidate type.
                /// </summary>
                ERROR_MRM_RESOURCE_TYPE_MISMATCH = 15117,
                /// <summary>
                /// Duplicate Resource Map.
                /// </summary>
                ERROR_MRM_DUPLICATE_MAP_NAME = 15118,
                /// <summary>
                /// Duplicate Entry.
                /// </summary>
                ERROR_MRM_DUPLICATE_ENTRY = 15119,
                /// <summary>
                /// Invalid Resource Identifier.
                /// </summary>
                ERROR_MRM_INVALID_RESOURCE_IDENTIFIER = 15120,
                /// <summary>
                /// Filepath too long.
                /// </summary>
                ERROR_MRM_FILEPATH_TOO_LONG = 15121,
                /// <summary>
                /// Unsupported directory type.
                /// </summary>
                ERROR_MRM_UNSUPPORTED_DIRECTORY_TYPE = 15122,
                /// <summary>
                /// Invalid PRI File.
                /// </summary>
                ERROR_MRM_INVALID_PRI_FILE = 15126,
                /// <summary>
                /// NamedResource Not Found.
                /// </summary>
                ERROR_MRM_NAMED_RESOURCE_NOT_FOUND = 15127,
                /// <summary>
                /// ResourceMap Not Found.
                /// </summary>
                ERROR_MRM_MAP_NOT_FOUND = 15135,
                /// <summary>
                /// Unsupported MRT profile type.
                /// </summary>
                ERROR_MRM_UNSUPPORTED_PROFILE_TYPE = 15136,
                /// <summary>
                /// Invalid qualifier operator.
                /// </summary>
                ERROR_MRM_INVALID_QUALIFIER_OPERATOR = 15137,
                /// <summary>
                /// Unable to determine qualifier value or qualifier value has not been set.
                /// </summary>
                ERROR_MRM_INDETERMINATE_QUALIFIER_VALUE = 15138,
                /// <summary>
                /// Automerge is enabled in the PRI file.
                /// </summary>
                ERROR_MRM_AUTOMERGE_ENABLED = 15139,
                /// <summary>
                /// Too many resources defined for package.
                /// </summary>
                ERROR_MRM_TOO_MANY_RESOURCES = 15140,
                /// <summary>
                /// The monitor returned a DDC/CI capabilities string that did not comply with the ACCESS.bus 3.0, DDC/CI 1.1 or MCCS 2 Revision 1 specification.
                /// </summary>
                ERROR_MCA_INVALID_CAPABILITIES_STRING = 15200,
                /// <summary>
                /// The monitor's VCP Version (0xDF) VCP code returned an invalid version value.
                /// </summary>
                ERROR_MCA_INVALID_VCP_VERSION = 15201,
                /// <summary>
                /// The monitor does not comply with the MCCS specification it claims to support.
                /// </summary>
                ERROR_MCA_MONITOR_VIOLATES_MCCS_SPECIFICATION = 15202,
                /// <summary>
                /// The MCCS version in a monitor's mccs_ver capability does not match the MCCS version the monitor reports when the VCP Version (0xDF) VCP code is used.
                /// </summary>
                ERROR_MCA_MCCS_VERSION_MISMATCH = 15203,
                /// <summary>
                /// The Monitor Configuration API only works with monitors that support the MCCS 1.0 specification, MCCS 2.0 specification or the MCCS 2.0 Revision 1 specification.
                /// </summary>
                ERROR_MCA_UNSUPPORTED_MCCS_VERSION = 15204,
                /// <summary>
                /// An internal Monitor Configuration API error occurred.
                /// </summary>
                ERROR_MCA_INTERNAL_ERROR = 15205,
                /// <summary>
                /// The monitor returned an invalid monitor technology type. CRT, Plasma and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.
                /// </summary>
                ERROR_MCA_INVALID_TECHNOLOGY_TYPE_RETURNED = 15206,
                /// <summary>
                /// The caller of SetMonitorColorTemperature specified a color temperature that the current monitor did not support. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.
                /// </summary>
                ERROR_MCA_UNSUPPORTED_COLOR_TEMPERATURE = 15207,
                /// <summary>
                /// The requested system device cannot be identified due to multiple indistinguishable devices potentially matching the identification criteria.
                /// </summary>
                ERROR_AMBIGUOUS_SYSTEM_DEVICE = 15250,
                /// <summary>
                /// The requested system device cannot be found.
                /// </summary>
                ERROR_SYSTEM_DEVICE_NOT_FOUND = 15299,
                /// <summary>
                /// Hash generation for the specified hash version and hash type is not enabled on the server.
                /// </summary>
                ERROR_HASH_NOT_SUPPORTED = 15300,
                /// <summary>
                /// The hash requested from the server is not available or no longer valid.
                /// </summary>
                ERROR_HASH_NOT_PRESENT = 15301,
                /// <summary>
                /// The secondary interrupt controller instance that manages the specified interrupt is not registered.
                /// </summary>
                ERROR_SECONDARY_IC_PROVIDER_NOT_REGISTERED = 15321,
                /// <summary>
                /// The information supplied by the GPIO client driver is invalid.
                /// </summary>
                ERROR_GPIO_CLIENT_INFORMATION_INVALID = 15322,
                /// <summary>
                /// The version specified by the GPIO client driver is not supported.
                /// </summary>
                ERROR_GPIO_VERSION_NOT_SUPPORTED = 15323,
                /// <summary>
                /// The registration packet supplied by the GPIO client driver is not valid.
                /// </summary>
                ERROR_GPIO_INVALID_REGISTRATION_PACKET = 15324,
                /// <summary>
                /// The requested operation is not suppported for the specified handle.
                /// </summary>
                ERROR_GPIO_OPERATION_DENIED = 15325,
                /// <summary>
                /// The requested connect mode conflicts with an existing mode on one or more of the specified pins.
                /// </summary>
                ERROR_GPIO_INCOMPATIBLE_CONNECT_MODE = 15326,
                /// <summary>
                /// The interrupt requested to be unmasked is not masked.
                /// </summary>
                ERROR_GPIO_INTERRUPT_ALREADY_UNMASKED = 15327,
                /// <summary>
                /// The requested run level switch cannot be completed successfully.
                /// </summary>
                ERROR_CANNOT_SWITCH_RUNLEVEL = 15400,
                /// <summary>
                /// The service has an invalid run level setting. The run level for a service must not be higher than the run level of its dependent services.
                /// </summary>
                ERROR_INVALID_RUNLEVEL_SETTING = 15401,
                /// <summary>
                /// The requested run level switch cannot be completed successfully since one or more services will not stop or restart within the specified timeout.
                /// </summary>
                ERROR_RUNLEVEL_SWITCH_TIMEOUT = 15402,
                /// <summary>
                /// A run level switch agent did not respond within the specified timeout.
                /// </summary>
                ERROR_RUNLEVEL_SWITCH_AGENT_TIMEOUT = 15403,
                /// <summary>
                /// A run level switch is currently in progress.
                /// </summary>
                ERROR_RUNLEVEL_SWITCH_IN_PROGRESS = 15404,
                /// <summary>
                /// One or more services failed to start during the service startup phase of a run level switch.
                /// </summary>
                ERROR_SERVICES_FAILED_AUTOSTART = 15405,
                /// <summary>
                /// The task stop request cannot be completed immediately since task needs more time to shutdown.
                /// </summary>
                ERROR_COM_TASK_STOP_PENDING = 15501,
                /// <summary>
                /// Package could not be opened.
                /// </summary>
                ERROR_INSTALL_OPEN_PACKAGE_FAILED = 15600,
                /// <summary>
                /// Package was not found.
                /// </summary>
                ERROR_INSTALL_PACKAGE_NOT_FOUND = 15601,
                /// <summary>
                /// Package data is invalid.
                /// </summary>
                ERROR_INSTALL_INVALID_PACKAGE = 15602,
                /// <summary>
                /// Package failed updates, dependency or conflict validation.
                /// </summary>
                ERROR_INSTALL_RESOLVE_DEPENDENCY_FAILED = 15603,
                /// <summary>
                /// There is not enough disk space on your computer. Please free up some space and try again.
                /// </summary>
                ERROR_INSTALL_OUT_OF_DISK_SPACE = 15604,
                /// <summary>
                /// There was a problem downloading your product.
                /// </summary>
                ERROR_INSTALL_NETWORK_FAILURE = 15605,
                /// <summary>
                /// Package could not be registered.
                /// </summary>
                ERROR_INSTALL_REGISTRATION_FAILURE = 15606,
                /// <summary>
                /// Package could not be unregistered.
                /// </summary>
                ERROR_INSTALL_DEREGISTRATION_FAILURE = 15607,
                /// <summary>
                /// User cancelled the install request.
                /// </summary>
                ERROR_INSTALL_CANCEL = 15608,
                /// <summary>
                /// Install failed. Please contact your software vendor.
                /// </summary>
                ERROR_INSTALL_FAILED = 15609,
                /// <summary>
                /// Removal failed. Please contact your software vendor.
                /// </summary>
                ERROR_REMOVE_FAILED = 15610,
                /// <summary>
                /// The provided package is already installed, and reinstallation of the package was blocked. Check the AppXDeployment-Server event log for details.
                /// </summary>
                ERROR_PACKAGE_ALREADY_EXISTS = 15611,
                /// <summary>
                /// The application cannot be started. Try reinstalling the application to fix the problem.
                /// </summary>
                ERROR_NEEDS_REMEDIATION = 15612,
                /// <summary>
                /// A Prerequisite for an install could not be satisfied.
                /// </summary>
                ERROR_INSTALL_PREREQUISITE_FAILED = 15613,
                /// <summary>
                /// The package repository is corrupted.
                /// </summary>
                ERROR_PACKAGE_REPOSITORY_CORRUPTED = 15614,
                /// <summary>
                /// To install this application you need either a Windows developer license or a sideloading-enabled system.
                /// </summary>
                ERROR_INSTALL_POLICY_FAILURE = 15615,
                /// <summary>
                /// The application cannot be started because it is currently updating.
                /// </summary>
                ERROR_PACKAGE_UPDATING = 15616,
                /// <summary>
                /// The package deployment operation is blocked by policy. Please contact your system administrator.
                /// </summary>
                ERROR_DEPLOYMENT_BLOCKED_BY_POLICY = 15617,
                /// <summary>
                /// The package could not be installed because resources it modifies are currently in use.
                /// </summary>
                ERROR_PACKAGES_IN_USE = 15618,
                /// <summary>
                /// The package could not be recovered because necessary data for recovery have been corrupted.
                /// </summary>
                ERROR_RECOVERY_FILE_CORRUPT = 15619,
                /// <summary>
                /// The signature is invalid. To register in developer mode, AppxSignature.p7x and AppxBlockMap.xml must be valid or should not be present.
                /// </summary>
                ERROR_INVALID_STAGED_SIGNATURE = 15620,
                /// <summary>
                /// An error occurred while deleting the package's previously existing application data.
                /// </summary>
                ERROR_DELETING_EXISTING_APPLICATIONDATA_STORE_FAILED = 15621,
                /// <summary>
                /// The package could not be installed because a higher version of this package is already installed.
                /// </summary>
                ERROR_INSTALL_PACKAGE_DOWNGRADE = 15622,
                /// <summary>
                /// An error in a system binary was detected. Try refreshing the PC to fix the problem.
                /// </summary>
                ERROR_SYSTEM_NEEDS_REMEDIATION = 15623,
                /// <summary>
                /// A corrupted CLR NGEN binary was detected on the system.
                /// </summary>
                ERROR_APPX_INTEGRITY_FAILURE_CLR_NGEN = 15624,
                /// <summary>
                /// The operation could not be resumed because necessary data for recovery have been corrupted.
                /// </summary>
                ERROR_RESILIENCY_FILE_CORRUPT = 15625,
                /// <summary>
                /// The package could not be installed because the Windows Firewall service is not running. Enable the Windows Firewall service and try again.
                /// </summary>
                ERROR_INSTALL_FIREWALL_SERVICE_NOT_RUNNING = 15626,
                /// <summary>
                /// The process has no package identity.
                /// </summary>
                APPMODEL_ERROR_NO_PACKAGE = 15700,
                /// <summary>
                /// The package runtime information is corrupted.
                /// </summary>
                APPMODEL_ERROR_PACKAGE_RUNTIME_CORRUPT = 15701,
                /// <summary>
                /// The package identity is corrupted.
                /// </summary>
                APPMODEL_ERROR_PACKAGE_IDENTITY_CORRUPT = 15702,
                /// <summary>
                /// The process has no application identity.
                /// </summary>
                APPMODEL_ERROR_NO_APPLICATION = 15703,
                /// <summary>
                /// Loading the state store failed.
                /// </summary>
                ERROR_STATE_LOAD_STORE_FAILED = 15800,
                /// <summary>
                /// Retrieving the state version for the application failed.
                /// </summary>
                ERROR_STATE_GET_VERSION_FAILED = 15801,
                /// <summary>
                /// Setting the state version for the application failed.
                /// </summary>
                ERROR_STATE_SET_VERSION_FAILED = 15802,
                /// <summary>
                /// Resetting the structured state of the application failed.
                /// </summary>
                ERROR_STATE_STRUCTURED_RESET_FAILED = 15803,
                /// <summary>
                /// State Manager failed to open the container.
                /// </summary>
                ERROR_STATE_OPEN_CONTAINER_FAILED = 15804,
                /// <summary>
                /// State Manager failed to create the container.
                /// </summary>
                ERROR_STATE_CREATE_CONTAINER_FAILED = 15805,
                /// <summary>
                /// State Manager failed to delete the container.
                /// </summary>
                ERROR_STATE_DELETE_CONTAINER_FAILED = 15806,
                /// <summary>
                /// State Manager failed to read the setting.
                /// </summary>
                ERROR_STATE_READ_SETTING_FAILED = 15807,
                /// <summary>
                /// State Manager failed to write the setting.
                /// </summary>
                ERROR_STATE_WRITE_SETTING_FAILED = 15808,
                /// <summary>
                /// State Manager failed to delete the setting.
                /// </summary>
                ERROR_STATE_DELETE_SETTING_FAILED = 15809,
                /// <summary>
                /// State Manager failed to query the setting.
                /// </summary>
                ERROR_STATE_QUERY_SETTING_FAILED = 15810,
                /// <summary>
                /// State Manager failed to read the composite setting.
                /// </summary>
                ERROR_STATE_READ_COMPOSITE_SETTING_FAILED = 15811,
                /// <summary>
                /// State Manager failed to write the composite setting.
                /// </summary>
                ERROR_STATE_WRITE_COMPOSITE_SETTING_FAILED = 15812,
                /// <summary>
                /// State Manager failed to enumerate the containers.
                /// </summary>
                ERROR_STATE_ENUMERATE_CONTAINER_FAILED = 15813,
                /// <summary>
                /// State Manager failed to enumerate the settings.
                /// </summary>
                ERROR_STATE_ENUMERATE_SETTINGS_FAILED = 15814,
                /// <summary>
                /// The size of the state manager composite setting value has exceeded the limit.
                /// </summary>
                ERROR_STATE_COMPOSITE_SETTING_VALUE_SIZE_LIMIT_EXCEEDED = 15815,
                /// <summary>
                /// The size of the state manager setting value has exceeded the limit.
                /// </summary>
                ERROR_STATE_SETTING_VALUE_SIZE_LIMIT_EXCEEDED = 15816,
                /// <summary>
                /// The length of the state manager setting name has exceeded the limit.
                /// </summary>
                ERROR_STATE_SETTING_NAME_SIZE_LIMIT_EXCEEDED = 15817,
                /// <summary>
                /// The length of the state manager container name has exceeded the limit.
                /// </summary>
                ERROR_STATE_CONTAINER_NAME_SIZE_LIMIT_EXCEEDED = 15818,
                /// <summary>
                /// This API cannot be used in the context of the caller's application type.
                /// </summary>
                ERROR_API_UNAVAILABLE = 15841
            }
        }
    }
}
