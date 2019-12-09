from oauth2client.service_account import ServiceAccountCredentials
import gspread
import argparse
import json

CONFIG_PATH = 'levels.json'
SERVICE_ACCOUNT_FILE_NAME = 'google_authentication.json'

DOCUMENT_NAME = 'Levels'
INFO_PAGE_NAME = 'Info'
FINISH_POSITION_VALUE = 'finish'

class Level:
    def __init__(self, table, rows, columns, finish):
        self.table = table
        self.rows = rows
        self.columns = columns
        self.finish = finish
    
    def Serialize(self):
        level = {}
        level['rows'] = self.rows
        level['columns'] = self.columns
        level['finish'] = self.finish
        level['table'] = []
        for (pos_i, row) in enumerate(self.table):
            for (pos_j, value) in enumerate(row):
                pos = {}
                pos['pos_i'] = pos_i
                pos['pos_j'] = pos_j
                pos['value'] = value
                level['table'].append(pos)
        return level

def parseArguments():
    parser = argparse.ArgumentParser(description="This script creates levels.json")
    parser.add_argument('-l', '--level', help = 'specify level page to convert')
    levelNumber = parser.parse_args().level

    if levelNumber:
        if not levelNumber.isdigit():
            raise Exception('Incorrect parameter: string')
        elif levelNumber.isdigit() and int(levelNumber) == 0:
            raise Exception('Incorrect parameter: zero')

    return levelNumber

def Authenticate():
    try:
        scope = ['https://spreadsheets.google.com/feeds',
                'https://www.googleapis.com/auth/drive']

        credentials = ServiceAccountCredentials.from_json_keyfile_name(
            SERVICE_ACCOUNT_FILE_NAME, scope)

        return gspread.authorize(credentials)

    except Exception:
        raise Exception('Google connection failed')

def LoadGoogleDoc(gc, levelNumber = None):
    try:
        spreadsheet = gc.open(DOCUMENT_NAME)
    
        sheets = []
        if levelNumber:
            numberOfPages = len(spreadsheet.worksheets()) - 1
            if int(levelNumber) > numberOfPages:
                err = Exception()
                err.customer = True
                raise err

            sheets.append(spreadsheet.get_worksheet(int(levelNumber)))
        else:
            sheets = spreadsheet.worksheets()
    
        levels = []
        for (number, sheet) in enumerate(sheets):
            if sheet.title == INFO_PAGE_NAME:
                continue
    
            table = sheet.get_all_values()
            rows = len(table)
            columns = len(table[0])
            finish = 0

            for (index, row) in enumerate(table):
                if (FINISH_POSITION_VALUE in row):
                    finish = index
                    break

            level = Level(table, rows, columns, finish)
            levels.append(level)

            if levelNumber:
                print('Level %d is loaded' % int(levelNumber))
            else:
                print('Level %d is loaded' % number)
        print()

        return levels

    except Exception as err:
        if not hasattr(err, 'customer'):
            raise Exception('Google connection failed')
        else:
            raise Exception('Incorrect parameter: only %d levels' % numberOfPages)

def CreateConfig(levels):
    fileData = []

    for level in levels:
        fileData.append(level.Serialize())

    file = open(CONFIG_PATH, 'w')
    file.write(json.dumps(fileData, indent = 4, sort_keys = False))
    file.close()

if __name__ == "__main__":
    try:
        gc = Authenticate()
        levelNumber = parseArguments()
        levels = LoadGoogleDoc(gc, levelNumber)
        CreateConfig(levels)

    except Exception as err:
        print(err)
    else:
        print('Successfully done!')
