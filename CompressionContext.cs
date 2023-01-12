namespace XMemCompress
{
    public class CompressionContext : IDisposable
    {
        private int _context;

        public CompressionContext( XMEMCODEC_TYPE codec = XMEMCODEC_TYPE.XMEMCODEC_LZX )
        {
            int ret;
            if ( ( ret = XCompress32.XMemCreateCompressionContext( codec, 0, 0, ref _context ) ) != 0 )
                throw new XCompressException( $"XMemCreateCompressionContext returned non-zero value {ret}." );
        }

        public void Reset()
        {
            int ret;
            if ( ( ret = XCompress32.XMemResetCompressionContext( _context ) ) != 0 )
                throw new XCompressException( $"XMemResetCompressionContext returned non-zero value {ret}." );
        }

        /// <summary>
        /// Compresses decompressed data.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="output">Where the compressed data will be put. This array will be resized to fit the data.</param>
        public void Compress( byte[] data, ref byte[] output )
        {
            var len = output.Length;
            int ret;
            if ( ( ret = XCompress32.XMemCompress( _context, output, ref len, data, data.Length ) ) != 0 )
                throw new XCompressException( $"XMemCompress returned non-zero value {ret}." );
            Array.Resize( ref output, len );
        }

        /// <summary>
        /// Compresses decompressed data.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="len">Length of the uncompressed data.</param>
        /// <returns>The compressed data.</returns>
        public byte[] Compress( byte[] data, int len )
        {
            var output = new byte[len];
            Compress( data, ref output );
            return output;
        }

        public void Dispose()
        {
            XCompress32.XMemDestroyCompressionContext( _context );
        }
    }
}
