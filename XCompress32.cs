using System.Runtime.InteropServices;

namespace XMemCompress
{
    public enum XMEMCODEC_TYPE : int
    {
        XMEMCODEC_DEFAULT = 0,
        XMEMCODEC_LZX = 1
    }

    // TODO: What are those parameters?
    // TODO: What are those flags?
    internal class XCompress32
    {
        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemCreateDecompressionContext( XMEMCODEC_TYPE codec, int param, int flags, ref int context );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemResetDecompressionContext( int context );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemDecompress( int context, byte[] dest, ref int destLen, byte[] src, int srcLen );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static void XMemDestroyDecompressionContext( int context );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemCreateCompressionContext( XMEMCODEC_TYPE codec, int param, int flags, ref int context );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemResetCompressionContext( int context );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static int XMemCompress( int context, byte[] dest, ref int destLen, byte[] src, int srcSize );

        [DllImport( "xcompress32.dll", CallingConvention = CallingConvention.StdCall )]
        public extern static void XMemDestroyCompressionContext( int context );
    }
}
