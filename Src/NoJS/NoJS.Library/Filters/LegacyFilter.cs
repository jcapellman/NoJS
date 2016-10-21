using System;
using System.IO;

namespace NoJS.Library.Filters {
    public class LegacyFilter : Stream {
        private Stream _shrink;

        public LegacyFilter(Stream shrink) {
            _shrink = shrink;
        }

        public override void Flush() {
            throw new NotImplementedException();
        }

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }
    }
}