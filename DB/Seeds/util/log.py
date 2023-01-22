import pprint
import json

pp = pprint.PrettyPrinter(indent=2, width=40)


def json_dump(obj):
    print(json.dumps(obj, indent=2, default=str))
