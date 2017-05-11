set swig=C:\swigwin-3.0.7\swig.exe
pushd .
cd rt\rtengine
%swig% -c++ -csharp -debug-tmsearch -namespace PhotoAssistant.Indexer.Wrappers -outdir ..\..\..\PhotoAssistant.Indexer\Wrappers ..\..\wrappers.i >> swig.log
popd
copy wrappers_wrap.cxx rt\rtengine\wrappers_wrap.cxx /Y
del wrappers_wrap.cxx