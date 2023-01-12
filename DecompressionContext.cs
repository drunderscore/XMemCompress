namespace XMemCompress
{
    public class DecompressionContext : IDisposable
    {
        private int _context;

        public DecompressionContext( XMEMCODEC_TYPE codec = XMEMCODEC_TYPE.XMEMCODEC_LZX )
        {
            int ret;
            if ( ( ret = XCompress32.XMemCreateDecompressionContext( codec, 0, 0, ref _context ) ) != 0 )
                throw new XCompressException( $"XMemCreateDecompressionContext returned non-zero value {ret}." );
        }

        public void Reset()
        {
            int ret;
            if ( ( ret = XCompress32.XMemResetDecompressionContext( _context ) ) != 0 )
                throw new XCompressException( $"XMemResetDecompressionContext returned non-zero value {ret}." );
        }

        /// <summary>
        /// Decompresses compressed data.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="output">Where the decompressed data will put.</param>
        /// <returns>The total size of the compressed data.</returns>
        public void Decompress( byte[] data, ref byte[] output )
        {
            var len = output.Length;
            int ret;
            if ( ( ret = XCompress32.XMemDecompress( _context, output, ref len, data, data.Length ) ) != 0 )
                throw new XCompressException( $"XMemDecompress returned non-zero value {ret}." );
            Array.Resize( ref output, len );
        }

        /// <summary>
        /// Decompresses compressed data.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="len">Length of the compressed data.</param>
        /// <returns>The decompressed data.</returns>
        public byte[] Decompress( byte[] data, int len )
        {
            var output = new byte[len];
            Decompress( data, ref output );
            return output;
        }

        public void Dispose()
        {
            XCompress32.XMemDestroyDecompressionContext( _context );
        }
    }
}
