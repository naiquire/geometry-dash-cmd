import base64
import gzip
from io import BytesIO
import sys

def decrypt_level_data(data_str):
    # Step 1: Base64 decode
    try:
        decoded = base64.b64decode(data_str)
    except Exception as e:
        return f"[Base64 Decode Failed] {str(e)}"

    # Step 2: Gzip decompress (most level strings are gzipped)
    try:
        with gzip.GzipFile(fileobj=BytesIO(decoded)) as f:
            return f.read().decode('utf-8')
    except Exception as e:
        return f"[Gzip Decompress Failed] {str(e)}"

if __name__ == "__main__":
    if len(sys.argv) > 1:
        level_data = sys.argv[1]
        print(decrypt_level_data(level_data))
    else:
        print("Usage: python decrypt_level.py <level_data_base64>")
