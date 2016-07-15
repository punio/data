# COOKPADをクロールした時につかったソース

C#で作ったコンソールアプリです。

エントリーポイント(Main())で

・WebページからDB(SQL Server)へ

・DBからテキストファイルへ

という処理を行っています。

## Web -> DB

SQL Serverに[Cookpad]データベースを作成してそこに[Recipe]テーブルがある状態で動きます。
接続設定はApp.configの"DefaultConnection"を使用します。

HtmlAgilityPackを使用して必要なデータを探してDapper使って保存しているだけです。

## DB -> Text

メソッドのはじめに保存先のパスが書いてあります。

Dapperで読んで各データをそのパスに出力しています。