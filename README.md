# Subtitles Translator

Translates .src subtitles using Yandex Translate API.

## Usage

1. Get Yandex Cloud FolderId and token.
Token can be obtained by running Yandex Cloud console api:

```
yc iam create-token
```

2. Build Subtitles Translator.

3. Run it:

```
dotnet SubtitlesTranslator.dll --SourceFile=<SourceFileName>.srt --YandexTranslateToken="<Token>" --YandexCloudFolderId="<FolderId>"
```


One more additional command line switch is `TargetLanguageCode` allows to set the language code of translation destination. Default value is `ru`.

### Example of translation

`en`:
```text
1
00:00:04,827 --> 00:00:07,625
<i>* From Mayfair to Park Lane</i>

2
00:00:07,667 --> 00:00:10,181
<i>* You will hear this same refrain</i>
```

`ru`:

```text
1
00:00:04,827 --> 00:00:07,625
<i> * от Мейфэра до Парк-Лейн</i>

2
00:00:07,667 --> 00:00:10,181
<i>* Вы услышите этот же рефрен</i>
```