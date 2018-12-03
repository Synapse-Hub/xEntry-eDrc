Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_terrain_reboiser_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_identification_pepiniere_v6 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_suivi_pepiniere_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'


Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de terrain à reboiser (v5)\instances" -g1'
Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche identification pepiniere (v6)\instances" -g3'
Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de suivi pépinière (v5)\instances" -g4'