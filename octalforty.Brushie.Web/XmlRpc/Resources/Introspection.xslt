<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" />

    <xsl:template match="/xmlrpc-service">
        <html>
            <head>
                <style type="text/css">
                    body { color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }
                    #content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }
                    a:link { color: #336699; font-weight: bold; text-decoration: underline; }
                    a:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }
                    a:active { color: #336699; font-weight: bold; text-decoration: underline; }
                    a:hover { color: cc3300; font-weight: bold; text-decoration: underline; }
                    p { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana; }
                    .heading1 { color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 105%; }
                    .intro { margin-left: -15px; }
                </style>
                <title><xsl:value-of select="./@name"/> XML-RPC Service</title>
            </head>
            <body>
                <div id="content">
                    <p class="heading1"><xsl:value-of select="./@name"/> XML-RPC Service</p>
                    <br />
                    <span>
                        <p class="intro"><xsl:value-of select="./@description"/></p>
                    </span>                    

                    <span>
                        <p class="intro">
                            The following operations are supported.
                        </p>
                        <ul>
        <xsl:for-each select="./methods/method">
                            <li><strong><xsl:value-of select="./@name"/></strong></li>
                            <span>
                                <br />
                                <xsl:value-of select="./@description"/>
                            </span>
        </xsl:for-each>
                        </ul>
                    </span>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>